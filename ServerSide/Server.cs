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
        Socket socket;
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

        public void RunServer()
        {
            try
            {
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.Bind(new IPEndPoint(IPAddress, port));
            }
            catch(SocketException e)
            {
                Debug.Print($"Server: Run error: {e.Message}");
                throw;
            }

            socket.Listen(0);
            AcceptConnectionsAsync();
            ListenClientsAsync();
        }

        public void StopServer()
        {
            if(isServerRunning)
            {
                isServerRunning = false;
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }
        }

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
                        while (senders[i].Socket.Available > 0)   //wait while client send data to server
                        {
                            Thread.Sleep(1);
                        }
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

        public void SendBroadcastAsync(string message)
        {
            Thread.Sleep(1);

            senders.Select(x => x.Login).Distinct().ToList().ForEach(login => SendAsync(login, message));
        }

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

                        senders.Add(new Sender(login, clientSocket));
                    }
                    catch (Exception e)
                    {
                        Debug.Print("Accept connection error:" + e.Message);
                    }
                }
            });
        }

        private async void ListenClientsAsync()
        {
            await Task.Run(() =>
            {
                while (isServerRunning)
                {
                    for (int i = 0; i < senders.Count; i++)
                    {
                        try
                        {
                            if (senders[i] != null && senders[i].Socket.Available > 0)
                            {
                                string data = ReadClientData(senders[i].Socket);
                                ReceivedMsg?.Invoke(senders[i], data);
                            }
                        }
                        catch (SocketException e)
                        {
                            Debug.Print($"Data handler error: {e.Message}");
                        }
                    }
                }
            });
        }

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

        public void Subscribe(ClientMessageHandler handler) => ReceivedMsg += handler;

        public void Describe(ClientMessageHandler handler) => ReceivedMsg -= handler;
    }
}
