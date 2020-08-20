using System;

namespace Excel.PropertiesExceptions
{
    [Serializable()]
    public class ExcelItemPositionException : ExcelPropertyException
    {
        public ExcelItemPositionException() : base() { }
        public ExcelItemPositionException(string message) : base(message) { }
        public ExcelItemPositionException(string message, Exception inner) : base(message, inner) { }

        protected ExcelItemPositionException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
