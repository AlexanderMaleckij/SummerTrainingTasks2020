using ClientSide;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;

namespace ServerSide.Tests
{
    [TestClass()]
    public class ServerTests
    {
        [TestMethod()]
        public void ServerConstructorStringIpAddrTest()
        {
            Server server = new Server("127.0.0.1", 70); //all data is valid
            Assert.ThrowsException<ArgumentException>(() => new Server("127.02.a45::fe0", 70)); //non-valid ip
            Assert.ThrowsException<ArgumentException>(() => new Server("127.0.0.1", 0));        //non-valid port  
            Assert.ThrowsException<ArgumentException>(() => new Server("127.0.0.1", 65536));    //non-valid port
        }

        [TestMethod()]
        public void ServerConstructorClassIpAddrTest()
        {
            Server server = new Server(IPAddress.Loopback, 70); //all data is valid
            Assert.ThrowsException<ArgumentException>(() => new Server(IPAddress.Loopback, 0));        //non-valid port  
            Assert.ThrowsException<ArgumentException>(() => new Server(IPAddress.Loopback, 65536));    //non-valid port
        }

        [TestMethod()]
        public void ServerRunStopSendSubscribeTest()
        {
            Server server = new Server(IPAddress.Loopback, 5432);
            ClientsLogger logger = new ClientsLogger();
            server.Subscribe(logger.Handler);

            Client client1 = new Client(IPAddress.Loopback, 5432, "someUser1");
            Client client2 = new Client(IPAddress.Loopback, 5432, "someUser2");
            List<string> client1ReceivedMessages = new List<string>();
            List<string> client2ReceivedMessages = new List<string>();
            client1.SubscribeHandler((string message) => client1ReceivedMessages.Add(message));
            client2.SubscribeHandler((string message) => client2ReceivedMessages.Add(message));

            server.RunServer();
            client1.Connect();
            client1.Disconnect();
            client2.Connect();
            client1.Connect();
            Thread.Sleep(100);
            //send messages
            client1.Send("I am client 1");
            client2.Send("I am client 2");
            client1.Send("Какой-то текст от клиента 1");
            client2.Send("Какой-то текст от клиента 2");
            server.SendAsync("someUser1", "message for 1st connected client");
            server.SendAsync("someUser2", "message for 2nd connected client");
            server.SendBroadcastAsync("msg for all");
            Thread.Sleep(300);    //wait while messages arrived and processed by Logger handler

            List<string> loggedMsgsFromClient1 = logger.GetSenderMessages("someUser1").Select(x => x.MessageText).ToList();
            List<string> loggedMsgsFromClient2 = logger.GetSenderMessages("someUser2").Select(x => x.MessageText).ToList();

            Assert.IsTrue(ListsEquals(new List<string> { "I am client 1", "Какой-то текст от клиента 1" }, loggedMsgsFromClient1));
            Assert.IsTrue(ListsEquals(new List<string> { "I am client 2", "Какой-то текст от клиента 2" }, loggedMsgsFromClient2));
            Assert.IsTrue(ListsEquals(new List<string> { "message for 1st connected client", "msg for all" }, client1ReceivedMessages));
            Assert.IsTrue(ListsEquals(new List<string> { "message for 2nd connected client", "msg for all" }, client2ReceivedMessages));

            client1.Disconnect();
            Thread.Sleep(10);
            client2.Disconnect();
            server.StopServer();
        }

        private static bool ListsEquals(List<string> list1, List<string> list2)
        {
            if(list1.Count != list2.Count)
            {
                return false;
            }
            if (list1 == null && list2 == null)
            {
                return true;
            }
            if (list1 == null || list2 == null)
            {
                return false;
            }
            for (int i = 1; i < list1.Count; i++)
            {
                if (list1[i] != list2[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}