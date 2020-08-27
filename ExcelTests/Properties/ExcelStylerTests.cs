using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.Office.Interop.Excel;
using Range = Microsoft.Office.Interop.Excel.Range;
using System.Runtime.InteropServices;

namespace Excel.Properties.Tests
{
    [TestClass()]
    public class ExcelStylerTests
    {
        [TestMethod()]
        public void ApplyStyleTest()
        {
            //obtain range for testing ExcelStyler class
            var app = new Application();
            var workbook = app.Workbooks.Add();
            var worksheet = (Worksheet)workbook.ActiveSheet;
            Range testRange = worksheet.get_Range(worksheet.Cells[1, 1], worksheet.Cells[2, 2]);

            //main part of the test
            ExcelStyler excelStyler = new ExcelStyler();
            excelStyler.HorizontalAlignment = XlHAlign.xlHAlignRight;
            excelStyler.VerticalAlignment = XlVAlign.xlVAlignTop;
            excelStyler.FontColor = XlRgbColor.rgbAquamarine;
            excelStyler.BackgroundColor = XlRgbColor.rgbBeige;
            excelStyler.Font = "Segoe UI";
            excelStyler.FontSize = 225;
            excelStyler.ApplyStyle(testRange);

            //checking the style
            Assert.AreEqual(XlHAlign.xlHAlignRight, (XlHAlign)testRange.HorizontalAlignment);
            Assert.AreEqual(XlVAlign.xlVAlignTop,   (XlVAlign)testRange.VerticalAlignment);
            Assert.AreEqual(XlRgbColor.rgbAquamarine, (XlRgbColor)(double)testRange.Font.Color);
            Assert.AreEqual(XlRgbColor.rgbBeige,      (XlRgbColor)(double)testRange.Interior.Color);
            Assert.AreEqual("Segoe UI", (string)testRange.Cells.Font.Name);
            Assert.AreEqual(225,        (double)testRange.Font.Size);

            //release resources
            app.Quit();
            Marshal.ReleaseComObject(app);
            Marshal.ReleaseComObject(workbook);
            Marshal.ReleaseComObject(worksheet);
            app = null;
            workbook = null;
            worksheet = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }
}