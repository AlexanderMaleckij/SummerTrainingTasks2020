using System.Net.Sockets;

namespace ServerSide
{
    public class Sender
    {
        public string Login { get; set; }
        public Socket Socket { get; private set; }

        public Sender(string login, Socket socket)
        {
            Login = login;
            Socket = socket;
        }
    }
}
