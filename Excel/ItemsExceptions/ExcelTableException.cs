using System;

namespace Excel.ItemsExceptions
{
    [Serializable()]
    class ExcelTableException : ExcelItemException
    {
        public ExcelTableException() : base() { }
        public ExcelTableException(string message) : base(message) { }
        public ExcelTableException(string message, Exception inner) : base(message, inner) { }

        protected ExcelTableException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
