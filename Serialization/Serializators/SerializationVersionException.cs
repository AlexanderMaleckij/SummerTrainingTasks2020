using System;

namespace Serialization.Serializators
{
    public class SerializationVersionException : Exception
    {
        public SerializationVersionException()
        {
        }

        public SerializationVersionException(string message)
            : base(message)
        {
        }

        public SerializationVersionException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
