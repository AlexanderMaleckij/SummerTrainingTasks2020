using System;
using System.Linq;
using System.Text;

namespace Excel
{
    public class ExcelItemPosition
    {
        private int x = 1;
        private int y = 1;

        public string CellCoordX
        {
            get => Convert(x);
            set
            {
                if (IsContainsNotEnglishLetters(value))
                {
                    throw new ExcelItemException("The numbering of cells along the x-axis in Excel in string form must contain only English letters");
                }

                x = Convert(value);
            }
        }
        public string CellCoordY
        {
            get => Convert(y);
            set
            {
                if (IsContainsNotEnglishLetters(value))
                {
                    throw new ExcelItemException("The numbering of cells along the y-axis in Excel in string form must contain only English letters");
                }

                y = Convert(value);
            }
        }
        public int CellCoordNumberX
        {
            get => x;
            set
            {
                if (value < 1)
                {
                    throw new ExcelItemException("Numbering of cells along the x-axis in Excel starts from 1");
                }

                x = value;
            }
        }
        public int CellCoordNumberY
        {
            get => y;
            set
            {
                if (value < 1)
                {
                    throw new ExcelItemException("Numbering of cells along the y-axis in Excel starts from 1");
                }

                y = value;
            }
        }

        private bool IsContainsNotEnglishLetters(string str)
        {
            return str.ToUpper().Where(x => x < 'A' || x > 'Z').Any();
        }

        private int Convert(string cellCoord)
        {
            string coord = cellCoord.ToUpper();
            int length = coord.Length;
            int result = default;

            for (int i = 0; i < length; i++)
            {
                int letterValue = (Encoding.ASCII.GetBytes(new char[] { coord[i] }).First() - 64);

                if (i == length - 1)
                {
                    result += letterValue;
                }
                else
                {
                    result += letterValue * (int)Math.Pow(26, length - i - 1);
                }
            }

            return result;
        }

        private string Convert(int cellCoord)
        {
            StringBuilder stringBuilder = new StringBuilder();

            void RecursiveConvert(StringBuilder sb, int dividend)
            {
                if (dividend == 0)
                {
                    return;
                }

                if (dividend <= 26)
                {
                    sb.Append(((char)(dividend + 64)).ToString());
                    return;
                }

                int quotient = dividend / 26;
                int remainder = dividend % 26;
                sb.Append(((char)(quotient + 64)).ToString());
                RecursiveConvert(sb, remainder);
            }

            RecursiveConvert(stringBuilder, cellCoord);
            return stringBuilder.ToString();
        }

        public ExcelItemPosition() { }

        public ExcelItemPosition(string cellCoordX, string cellCoordY)
        {
            CellCoordX = cellCoordX;
            CellCoordY = cellCoordY;
        }

        public ExcelItemPosition(int cellCoordNumberX, int cellCoordNumberY)
        {
            CellCoordNumberX = cellCoordNumberX;
            CellCoordNumberY = cellCoordNumberY;
        }
    }
}
