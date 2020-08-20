using Excel.ItemsExceptions;
using Excel.Properties;
using Microsoft.Office.Interop.Excel;

namespace Excel.Items
{
    public abstract class ExcelItem
    {
        private ExcelItemPosition position = new ExcelItemPosition();

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

        public abstract void AddItem(Worksheet worksheet);
    }
}
