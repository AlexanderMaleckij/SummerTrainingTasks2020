using System;

namespace Excel.ItemsExceptions
{
    [Serializable()]
    class ExcelTextException : ExcelItemException
    {
        public ExcelTextException() : base() { }
        public ExcelTextException(string message) : base(message) { }
        public ExcelTextException(string message, Exception inner) : base(message, inner) { }

        protected ExcelTextException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
