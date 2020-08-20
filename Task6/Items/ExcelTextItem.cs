﻿using Excel.ItemsProperties;
using Microsoft.Office.Interop.Excel;

namespace Excel.Items
{
    public class ExcelTextItem : ExcelItem
    {
        private string text;
        private ExcelItemSize size = new ExcelItemSize();
        private ExcelStyler styler = new ExcelStyler();
        public ExcelItemSize Size
        {
            get => size;
            set
            {
                if (value == null)
                {
                    throw new ExcelItemException("Excel item size can't be null");
                }

                size = value;
            }
        }
        public ExcelStyler Styler
        {
            get => styler;
            set
            {
                if (value == null)
                {
                    throw new ExcelItemException("Style can't be null");
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
                    throw new ExcelItemException("Text cant be null or empty");
                }

                text = value;
            }
        }
        public ExcelTextItem(string text)
        {
            Text = text;
        }

        public override void AddItem(Worksheet worksheet)
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
