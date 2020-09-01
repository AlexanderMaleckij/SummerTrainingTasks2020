using Excel.PropertiesExceptions;
using Microsoft.Office.Interop.Excel;

namespace Excel.Properties
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
                    throw new ExcelStylerException("Font size can't be less than 1");
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
                    throw new ExcelStylerException("Font name can't be null or empty");
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

        public override bool Equals(object obj)
        {
            if (obj != null && obj is ExcelStyler)
            {
                ExcelStyler styler = obj as ExcelStyler;
                if (styler.BackgroundColor == BackgroundColor &&
                    styler.Font == Font &&
                    styler.FontColor == FontColor &&
                    styler.FontSize == FontSize &&
                    styler.HorizontalAlignment == HorizontalAlignment &&
                    styler.VerticalAlignment == VerticalAlignment)
                {
                    return true;
                }
            }

            return false;
        }

        public override int GetHashCode()
        {
            return (BackgroundColor.GetHashCode() << 6) ^
                (Font.GetHashCode() << 5) ^
                (FontColor.GetHashCode() << 4) ^
                (FontSize.GetHashCode() << 3) ^
                (HorizontalAlignment.GetHashCode() << 2) ^
                VerticalAlignment.GetHashCode();
        }

        public override string ToString()
        {
            return $"{nameof(ExcelStyler)} " +
                $"{nameof(BackgroundColor)} = {BackgroundColor}; " +
                $"{nameof(Font)} = {Font}; " +
                $"{nameof(FontColor)} = {FontColor}; " +
                $"{nameof(FontSize)} = {FontSize}; " +
                $"{nameof(HorizontalAlignment)} = {HorizontalAlignment}; " +
                $"{nameof(VerticalAlignment)} = {VerticalAlignment}";
        }
    }
}
