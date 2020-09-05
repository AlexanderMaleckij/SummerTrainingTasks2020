using Microsoft.Office.Interop.Excel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Excel
{
    public class ExcelAssert
    {
        /// <summary>
        /// Compares the text in the specified Excel documents in the specified area
        /// </summary>
        /// <param name="reportExpectedFileName">report 1 file name</param>
        /// <param name="reportActualFileName">report 2 file name</param>
        public static void ReportsTextSame(string reportExpectedFileName, string reportActualFileName)
        {
            var report1 = new ExcelReport(reportExpectedFileName);
            var report2 = new ExcelReport(reportActualFileName);
            CollectionAssert.AreEqual(GetCellsText(report1.worksheet), GetCellsText(report2.worksheet));
            report1.Dispose();
            report2.Dispose();
        }

        private static List<string> GetCellsText(Worksheet worksheet)
        {
            Range range = worksheet.UsedRange;
            List<string> reportCellsValues = new List<string>();
            foreach (Range cell in range.Rows.Cells)
            {
                reportCellsValues.Add((string)cell.Text);
            }
            return reportCellsValues;
        }

    }
}
