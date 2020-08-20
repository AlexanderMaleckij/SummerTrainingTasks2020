using System;

namespace Excel
{
    [Serializable()]
    public class ExcelItemException : Exception
    {
        public ExcelItemException() : base() { }
        public ExcelItemException(string message) : base(message) { }
        public ExcelItemException(string message, Exception inner) : base(message, inner) { }

        // A constructor is needed for serialization when an
        // exception propagates from a remoting server to the client.
        protected ExcelItemException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
