using System;

namespace Excel.ContainersExceptions
{
    [Serializable()]
    public class ExcelStackPanelException : Exception
    {
        public ExcelStackPanelException() : base() { }
        public ExcelStackPanelException(string message) : base(message) { }
        public ExcelStackPanelException(string message, Exception inner) : base(message, inner) { }

        protected ExcelStackPanelException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
