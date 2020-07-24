using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ClientSide
{
    public delegate void ServerMessagesHandler(string message);
    public class Client
    {
        private event ServerMessagesHandler ReceivedMsg;
        Socket socket;
        private int port;
        private string login;
        private bool isActive = false;
        public Encoding ClientEncoding { get; private set; } = Encoding.Unicode;

        public IPAddress IPAddress { get; private set; }

        public string Login
        {
            get => login;
            private set
            {
                if(string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Login can't be null or empty");
                }

                if(value.Any(x => char.IsWhiteSpace(x)))
                {
                    throw new ArgumentException("Login can't contain any whitespaces");
                }

                login = value;
            }
        }

        public int Port
        {
            get => port;
            private set
            {
                if(value < 1 || value > 65535)
                {
                    throw new ArgumentException("Port value must be in range (1-65535)");
                }
                else
                {
                    port = value;
                }
            }
        }

        public Client(IPAddress iPAddress, int port, string login)
        {
            IPAddress = iPAddress;
            Port = port;
            Login = login;
        }

        public Client(string iPAddress, int port, string login)
        {
            if(IPAddress.TryParse(iPAddress, out IPAddress parsedIp))
            {
                IPAddress = parsedIp;
            }
            else
            {
                throw new ArgumentException("IPv4 address is not valid");
            }
            Port = port;
            Login = login;
        }

        public void Connect()
        {
            try
            {
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);   //use IPv4 TCP Socket
                socket.Connect(IPAddress, Port);
                socket.Send(ClientEncoding.GetBytes(login));    //without queue for quick send
                ListenServerAsync();
            }
            catch(SocketException e)
            {
                Debug.Print($"Client: Server connection error: {e.Message}");
                throw;
            }
        }

        /// <summary>
        /// For disconneting from server
        /// </summary>
        public void Disconnect()
        {
            if(socket.Connected)
            {
                isActive = false;
                socket.Shutdown(SocketShutdown.Both);        //waiting for the end of data transfer
                socket.Close();     //closing connection and disposing resources used by the socket
            }
        }

        /// <summary>
        /// For sending data to server
        /// </summary>
        /// <param name="message"></param>
        public void Send(string message)
        {
            byte[] buffer = Encoding.Unicode.GetBytes(message);
            socket.Send(buffer);
            Thread.Sleep(2);    //so that 2 messages sent in a row do not merge
        }

        /// <summary>
        /// receive messages, invoke event subscribers
        /// </summary>
        private async void ListenServerAsync()
        {
            isActive = true;

            await Task.Run(() => {

                StringBuilder sb = new StringBuilder();

                while (isActive)
                {
                    try
                    {
                        do
                        {
                            byte[] buffer = new byte[256];
                            int receivedBytesAmount = socket.Receive(buffer);
                            sb.Append(ClientEncoding.GetString(buffer, 0, receivedBytesAmount));
                        }
                        while (socket.Available > 0);

                        ReceivedMsg?.Invoke(sb.ToString()); //notify all event subscribers (if they are)
                        Debug.Print($"Client: receive message: {sb}");
                        sb.Clear();         //clear StringBuilder instance from received message
                    }
                    catch(Exception e)
                    {
                        if (isActive)
                        {
                            Debug.Print($"Client: {e.Message}");
                        }
                    }
                }
            });
        }

        /// <summary>
        /// Subscribes a handler to process new messages from the server
        /// </summary>
        /// <param name="handler">incoming messages handler</param>
        public void SubscribeHandler(ServerMessagesHandler handler) => ReceivedMsg += handler;

        /// <summary>
        /// Debscribes a handler from processing new messages from the server
        /// </summary>
        /// <param name="handler">incoming messages handler</param>
        public void DescribeHandler(ServerMessagesHandler handler) =>  ReceivedMsg -= handler;
    }
}
