using Excel.PropertiesExceptions;

namespace Excel.Properties
{
    public class ExcelItemSize
    {
        private int width = 1;
        private int height = 1;

        public int Width
        {
            get => width;
            set
            {
                if (width < 1)
                {
                    throw new ExcelItemSizeException("Excel item width can't be less than 1");
                }

                width = value;
            }
        }

        public int Height
        {
            get => height;
            set
            {
                if (height < 1)
                {
                    throw new ExcelItemSizeException("Excel item hright can't be less than 1");
                }

                height = value;
            }
        }

        public ExcelItemSize() { }

        public ExcelItemSize(int width, int height)
        {
            Width = width;
            Height = height;
        }
    }
}
