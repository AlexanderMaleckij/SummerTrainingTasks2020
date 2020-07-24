using System.Collections.Generic;
using System.Linq;

namespace ServerSide
{
    /// <summary>
    /// Class for collecting messages from clients
    /// </summary>
    public class ClientsLogger
    {
        public ClientMessageHandler Handler { get; private set; }

        List<SenderMessages> activeSendersMessages = new List<SenderMessages>();

        /// <summary>
        /// Class, that represent one sender and it's messages
        /// </summary>
        class SenderMessages
        {
            public List<Message> Messages { get; private set; } = new List<Message>();
            public Sender Sender { get; private set; }

            public SenderMessages(Sender sender)
            {
                Sender = sender;
            }

            public void AddMessage(string text) => Messages.Add(new Message(text));
        }

        public ClientsLogger()
        {
            Handler = delegate (Sender sender, string message)
            {
                if (!activeSendersMessages.Select(x => x.Sender).Contains(sender))
                {
                    activeSendersMessages.Add(new SenderMessages(sender));
                }

                activeSendersMessages.Where(x => x.Sender == sender).First().AddMessage(message);
            };
        }

        /// <summary>
        /// Get all messages of all clients with specified login
        /// </summary>
        /// <param name="login">login to search for messages</param>
        /// <returns>messages related to a client(s) with specified login</returns>
        public List<Message> GetSenderMessages(string login)
        {
            List<SenderMessages> senderMessages = activeSendersMessages.Where(x => x.Sender.Login == login).ToList();
            List<Message> sendersMessages = new List<Message>();

            foreach(SenderMessages messages in senderMessages)
            {
                if(messages != null)
                {
                    sendersMessages.AddRange(messages.Messages);
                }
            }

            return sendersMessages;
        }
    }
}
