using System;
using System.Runtime.Serialization;

namespace ColorMaterial
{
    [Serializable()]
    public class ColorationException : Exception
    {
        public ColorationException() { }

        public ColorationException(string message) : base(message) { }

        public ColorationException(string message, Exception inner) : base(message, inner) { }

        // A constructor is needed for serialization when an
        // exception propagates from a remoting server to the client.
        protected ColorationException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
