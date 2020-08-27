using Excel.ItemsExceptions;
using Excel.Properties;
using Microsoft.Office.Interop.Excel;

namespace Excel.Items
{
    public class ExcelText : ExcelItem
    {
        private string text;
        private ExcelStyler styler = new ExcelStyler();

        public ExcelStyler Styler
        {
            get => styler;
            set
            {
                if (value == null)
                {
                    throw new ExcelTextException("Style can't be null");
                }

                styler = value;
            }
        }
        public string Text
        {
            get => text;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ExcelTextException("Text cant be null or empty");
                }

                text = value;
            }
        }
        public ExcelText(string text)
        {
            Text = text;
        }

        internal override void AddItem(Worksheet worksheet)
        {
            worksheet.Cells[Position.CellCoordNumberY, Position.CellCoordNumberX] = Text;
            styler.ApplyStyle((Range)worksheet.Cells[Position.CellCoordNumberY, Position.CellCoordNumberX]);

            Range leftUpCorner = (Range)worksheet.Cells[Position.CellCoordNumberY,
                                                       Position.CellCoordNumberX];

            Range rightDownCorner = (Range)worksheet.Cells[Position.CellCoordNumberY + size.Height - 1,
                                                          Position.CellCoordNumberX + size.Width - 1];

            worksheet.get_Range(leftUpCorner, rightDownCorner).Merge();
        }
    }
}
