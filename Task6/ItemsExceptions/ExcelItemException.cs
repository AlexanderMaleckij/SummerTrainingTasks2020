using System;

namespace Excel.ItemsExceptions
{
    [Serializable()]
    public class ExcelItemException : Exception
    {
        public ExcelItemException() : base() { }
        public ExcelItemException(string message) : base(message) { }
        public ExcelItemException(string message, Exception inner) : base(message, inner) { }

        protected ExcelItemException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
