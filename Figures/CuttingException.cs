using System;
using System.Runtime.Serialization;

namespace Figures
{
    [Serializable]
    class CuttingException : Exception
    {
        public CuttingException() { }

        public CuttingException(string message) : base(message) { }

        public CuttingException(string message, Exception inner) : base(message, inner) { }

        // A constructor is needed for serialization when an
        // exception propagates from a remoting server to the client.
        protected CuttingException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
