using Excel.ItemsExceptions;
using Excel.Properties;
using Microsoft.Office.Interop.Excel;

namespace Excel.Items
{
    /// <summary>
    /// Base class for all Excel items
    /// </summary>
    public abstract class ExcelItem
    {
        private ExcelItemPosition position = new ExcelItemPosition();
        protected ExcelItemSize size = new ExcelItemSize();

        public ExcelItemSize Size
        {
            get => size;
            set
            {
                if (value == null)
                {
                    throw new ExcelItemException("Excel item size can't be null");
                }

                Size = value;
            }
        }

        public ExcelItemPosition Position
        {
            get => position;
            set
            {
                if (value == null)
                {
                    throw new ExcelItemException("Excel item position can't be null");
                }

                position = value;
            }
        }

        internal abstract void AddItem(Worksheet worksheet);
    }
}
