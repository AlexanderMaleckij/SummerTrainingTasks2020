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
        private bool isListen = false;

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
                Send(Login);
                Thread.Sleep(10);   //wait while server accept connection
                ListenServerAsync();
            }
            catch(SocketException e)
            {
                Debug.Print($"Client: Server connection error: {e.Message}");
                throw;
            }
        }

        public void Disconnect()
        {
            if(socket.Connected)
            {
                isListen = false;
                socket.Shutdown(SocketShutdown.Both);        //waiting for the end of data transfer
                socket.Close();     //closing connection and disposing resources used by the socket
            }
        }

        public void Send(string message)
        {
            socket.Send(ClientEncoding.GetBytes(message));
            Thread.Sleep(2);    //so that 2 messages sent in a row don't merge
        }

        private async void ListenServerAsync()
        {
            isListen = true;

            await Task.Run(() => {

                StringBuilder sb = new StringBuilder();

                while (isListen)
                {
                    try
                    {
                        do
                        {
                            byte[] buffer = new byte[64];
                            int receivedBytesAmount = socket.Receive(buffer);
                            sb.Append(ClientEncoding.GetString(buffer, 0, receivedBytesAmount));
                        }
                        while (socket.Available > 0);

                        ReceivedMsg?.Invoke(sb.ToString()); //notify all event subscribers (if they are)
                        Debug.Print($"Client: receive messge: {sb}");
                        sb.Clear();         //clear StringBuilder instance from received message
                        Thread.Sleep(1);    //so as not to load the CPU by 100%
                    }
                    catch
                    {
                        if (isListen)
                        {
                            throw;
                        }
                    }
                }
            });
        }

        public void SubscribeHandler(ServerMessagesHandler handler) => ReceivedMsg += handler;

        public void DescribeHandler(ServerMessagesHandler handler) =>  ReceivedMsg -= handler;
    }
}
