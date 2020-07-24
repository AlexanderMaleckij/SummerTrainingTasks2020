using System;
using System.Collections.Generic;
using System.Text;

namespace ServerSide
{
    public class Message
    {
        public string MessageText { get; private set; }
        public DateTime MessageDateTime { get; private set; }

        public Message(string message)
        {
            MessageText = message;
            MessageDateTime = DateTime.UtcNow;
        }
    }
}
