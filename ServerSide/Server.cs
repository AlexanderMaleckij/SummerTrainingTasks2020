using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServerSide
{
    public delegate void ClientMessageHandler(Sender sender, string message);
    public class Server
    {
        public event ClientMessageHandler ReceivedMsg;
        public List<Sender> senders = new List<Sender>();
        Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        int port;
        bool isServerRunning = false;

        public Encoding ServerEncoding { get; private set; } = Encoding.Unicode;
        public IPAddress IPAddress { get; private set; }
        public int Port
        {
            get => port;
            set
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

        public Server(IPAddress iPAddress, int port)
        {
            IPAddress = iPAddress;
            Port = port;
        }

        public Server(string iPAddress, int port)
        {
            if (IPAddress.TryParse(iPAddress, out IPAddress parsedIPAddress))
            {
                IPAddress = parsedIPAddress;
            }
            else
            {
                throw new ArgumentException("IPv4 address not valid!");
            }
            Port = port;
        }

        /// <summary>
        /// Method for start the server
        /// starts listening on a socket and allows connections
        /// </summary>
        public void RunServer()
        {
            try
            {
                socket.Bind(new IPEndPoint(IPAddress, port));
            }
            catch(SocketException e)
            {
                Debug.Print($"Server: Run error: {e.Message}");
                throw;
            }

            socket.Listen(0);
            AcceptConnectionsAsync();
        }

        /// <summary>
        /// Server stop method (close socket)
        /// </summary>
        public void StopServer()
        {
            if(isServerRunning)
            {
                isServerRunning = false;
                socket.Close(1000);
            }
        }

        /// <summary>
        /// Method for sending a message to the client with the specified login
        /// </summary>
        /// <param name="recieverLogin">recipient login</param>
        /// <param name="message">text of sending message</param>
        public async void SendAsync(string recieverLogin, string message)
        {
            await Task.Run(() =>
            {
                List<Sender> senders = this.senders.Where(x => x.Login == recieverLogin).ToList();

                for (int i = 0; i < senders.Count; i++)
                {
                    int exceptionCounter = 0;
                    try
                    {
                        senders[i].Socket.Send(ServerEncoding.GetBytes(message));
                    }
                    catch (SocketException)
                    {
                        exceptionCounter++;
                        Thread.Sleep(1);

                        if (i != 0)
                        {
                            i--;
                        }
                    }
                }
            });
        }

        /// <summary>
        /// Method for sending a message with a same text to all attached clients 
        /// </summary>
        /// <param name="message">text of sending message</param>
        public void SendBroadcastAsync(string message)
        {
            Thread.Sleep(5);
            senders.Select(x => x.Login).Distinct().ToList().ForEach(login => SendAsync(login, message));
        }

        /// <summary>
        /// Method for accepting clients connections
        /// </summary>
        private async void AcceptConnectionsAsync()
        {
            isServerRunning = true;

            await Task.Run(() => {
                while (isServerRunning)
                {
                    try
                    {
                        Socket clientSocket = socket.Accept();
                        string login = ReadClientData(clientSocket);
                        Sender sender = new Sender(login, clientSocket);
                        senders.Add(sender);
                        ListenClientAsync(sender);
                    }
                    catch (Exception e)
                    {
                        Debug.Print("Accept connection error:" + e.Message);
                    }
                }
            });
        }

        /// <summary>
        /// Method for listen data from 1 client asynchronously
        /// </summary>
        /// <param name="sender"></param>
        private async void ListenClientAsync(Sender sender)
        {
            await Task.Run(() =>
            {
                while (isServerRunning)
                {
                    try
                    {
                        if (sender != null && sender.Socket.Available > 0)
                        {
                            string data = ReadClientData(sender.Socket);
                            ReceivedMsg?.Invoke(sender, data);
                        }
                    }
                    catch (SocketException e)
                    {
                        Debug.Print($"Data handler error: {e.Message}");
                    }
                }
            });
        }

        /// <summary>
        /// Method for reading message from specified client
        /// </summary>
        /// <param name="socket">sender socket</param>
        /// <returns>read message</returns>
        private string ReadClientData(Socket socket)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                int bytes = 0;
                byte[] buffer = new byte[256];

                do
                {
                    bytes = socket.Receive(buffer);
                    sb.Append(ServerEncoding.GetString(buffer, 0, bytes));
                }
                while (socket.Available > 0);

                return sb.ToString();
            }
            catch (Exception e)
            {
                Debug.Print($"Server: Client processing error: {e.Message}");
                throw;
            }
        }

        /// <summary>
        /// Subscribes a handler to process new messages from the clients
        /// </summary>
        /// <param name="handler">incoming messages handler</param>
        public void Subscribe(ClientMessageHandler handler) => ReceivedMsg += handler;

        /// <summary>
        /// Debscribes a handler from processing new messages from the clients
        /// </summary>
        /// <param name="handler">incoming messages handler</param>
        public void Describe(ClientMessageHandler handler) => ReceivedMsg -= handler;
    }
}
