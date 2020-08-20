using System;

namespace Excel.PropertiesExceptions
{
    [Serializable()]
    public class ExcelItemSizeException : ExcelPropertyException
    {
        public ExcelItemSizeException() : base() { }
        public ExcelItemSizeException(string message) : base(message) { }
        public ExcelItemSizeException(string message, Exception inner) : base(message, inner) { }

        protected ExcelItemSizeException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
