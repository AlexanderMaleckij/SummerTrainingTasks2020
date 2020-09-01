using System;

namespace Excel.PropertiesExceptions
{
    [Serializable()]
    public class ExcelStylerException : ExcelPropertyException
    {
        public ExcelStylerException() : base() { }
        public ExcelStylerException(string message) : base(message) { }
        public ExcelStylerException(string message, Exception inner) : base(message, inner) { }

        protected ExcelStylerException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
