using System;

namespace ServerSide
{
    /// <summary>
    /// Represent a message, that contains text and date
    /// </summary>
    public class Message
    {
        public string MessageText { get; private set; }
        public DateTime MessageDateTime { get; private set; }

        /// <summary>
        /// Create a Message class instance with specified 
        /// text of message and current date and time 
        /// </summary>
        /// <param name="message">Message class instance</param>
        public Message(string message)
        {
            MessageText = message;
            MessageDateTime = DateTime.UtcNow;
        }
    }
}
