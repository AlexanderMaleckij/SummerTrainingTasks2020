using System;
using Microsoft.Office.Interop.Excel;

namespace Excel.ItemsProperties
{
    public class ExcelStyler
    {
        private int fontSize = 11;
        private string font = "Calibri";
        public int FontSize
        {
            get => fontSize;
            set
            {
                if (value < 1)
                {
                    throw new ArgumentException("Font size can't be less than 1");
                }

                fontSize = value;
            }
        }
        public string Font
        {
            get => font;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Font name can't be null or empty");
                }

                font = value;
            }
        }
        public XlHAlign HorizontalAlignment { get; set; } = XlHAlign.xlHAlignLeft;
        public XlVAlign VerticalAlignment { get; set; } = XlVAlign.xlVAlignBottom;
        public XlRgbColor BackgroundColor { get; set; } = XlRgbColor.rgbWhite;
        public XlRgbColor FontColor { get; set; } = XlRgbColor.rgbBlack;

        public void ApplyStyle(Range range)
        {
            range.Font.Size = fontSize;
            range.HorizontalAlignment = HorizontalAlignment;
            range.VerticalAlignment = VerticalAlignment;
            range.Interior.Color = BackgroundColor;
            range.Font.Color = FontColor;
            range.Cells.Font.Name = Font;
        }
    }
}
