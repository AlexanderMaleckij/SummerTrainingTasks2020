using System;

namespace Excel.PropertiesExceptions
{
    [Serializable()]
    public class ExcelPropertyException : Exception
    {
        public ExcelPropertyException() : base() { }
        public ExcelPropertyException(string message) : base(message) { }
        public ExcelPropertyException(string message, Exception inner) : base(message, inner) { }

        protected ExcelPropertyException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
