using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServerSide;
using System;
using System.Net;
using System.Threading;

namespace ClientSide.Tests
{
    [TestClass()]
    public class ClientTests
    {
        [TestMethod()]
        public void ClientConstructorStringIpAddrTest()
        {
            Client client = new Client("127.0.0.1", 70, "Alexander");   //all data is valid
            Assert.IsNotNull(client);

            Assert.ThrowsException<ArgumentException>(() => new Client("127.02.a45::fe0", 80, "Alexander"));    //non-valid ip
            Assert.ThrowsException<ArgumentException>(() => new Client("127.0.0.1", 0, "Alexander"));           //non-valid port
            Assert.ThrowsException<ArgumentException>(() => new Client("127.0.0.1", 65536, "Alexander"));       //non-valid port

            Assert.ThrowsException<ArgumentException>(() => new Client("127.0.0.1", 443, "A l e x"));   //non-valid login (spaces)
            Assert.ThrowsException<ArgumentException>(() => new Client("127.0.0.1", 443, null));        //non-valid login (null)
            Assert.ThrowsException<ArgumentException>(() => new Client("127.0.0.1", 443, ""));          //non-valid login (empty)
        }

        [TestMethod()]
        public void ClientConstructorClassIpAddrTest()
        {
            Client client = new Client(IPAddress.Loopback, 443, "Alexander"); //all data is valid
            Assert.IsNotNull(client);
            Assert.ThrowsException<ArgumentException>(() => new Client(IPAddress.Loopback, 0, "Alexander"));        //non-valid port
            Assert.ThrowsException<ArgumentException>(() => new Client(IPAddress.Loopback, 65536, "Alexander"));    //non-valid port  

            Assert.ThrowsException<ArgumentException>(() => new Client(IPAddress.Loopback, 443, "A l e x"));  //non-valid login (spaces)
            Assert.ThrowsException<ArgumentException>(() => new Client(IPAddress.Loopback, 443, null));       //non-valid login (null)
            Assert.ThrowsException<ArgumentException>(() => new Client(IPAddress.Loopback, 443, ""));         //non-valid login (empty)
        }

        [TestMethod()]
        public void ConnectDisconnectSubscribeSendTests()
        {
            Client client = new Client(IPAddress.Loopback, 7896, "Alexander");
            Server server = new Server(IPAddress.Loopback, 7896);

            //conect test part
            server.RunServer();
            client.Connect();

            //receive test part
            string receivedTransodedMsg = string.Empty;
            client.SubscribeHandler((string message) =>     //subscribe lambda handler
            {
                receivedTransodedMsg = Transcoder.Transcode(message);
            });
            server.SendBroadcastAsync("Hello, world!");
            Thread.Sleep(5);
            Assert.AreEqual(Transcoder.Transcode("Hello, world!"), receivedTransodedMsg);
            server.SendAsync("Alexander", "Сообщение 1-му подключившемуся.");
            Thread.Sleep(5);
            Assert.AreEqual(Transcoder.Transcode("Сообщение 1-му подключившемуся."), receivedTransodedMsg);


            //send test part
            string msgReceivedFromClient = string.Empty;
            ClientMessageHandler serverHandler = delegate (Sender sender, string message)
            {
                msgReceivedFromClient = message;
            };
            server.Subscribe(serverHandler);    //subscribe anonymous method handler

            client.Send(receivedTransodedMsg);
            Thread.Sleep(5);
            Assert.AreEqual(receivedTransodedMsg, msgReceivedFromClient);

            //disconnect test part
            client.Disconnect();
            server.StopServer();
        }
    }
}