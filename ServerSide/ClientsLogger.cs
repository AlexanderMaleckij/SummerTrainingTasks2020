using System.Collections.Generic;
using System.Linq;

namespace ServerSide
{
    public class ClientsLogger
    {
        public ClientMessageHandler Handler { get; private set; }

        List<SenderMessages> activeSendersMessages = new List<SenderMessages>();

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
