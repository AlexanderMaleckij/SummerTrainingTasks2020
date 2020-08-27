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
                if (value < 1)
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
                if (value < 1)
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

        public override bool Equals(object obj)
        {
            if(obj != null && obj is ExcelItemSize)
            {
                ExcelItemSize size = obj as ExcelItemSize;
                if(size.Height == Height &&
                   size.Width == Width)
                {
                    return true;
                }
            }

            return false;
        }

        public override int GetHashCode()
        {
            return (Width.GetHashCode() << 2) ^ Height.GetHashCode();
        }

        public override string ToString()
        {
            return $"{nameof(ExcelItemSize)} " +
                $"{nameof(Width)} = {Width}; " +
                $"{nameof(Height)} = {Height}";
        }
    }
}
