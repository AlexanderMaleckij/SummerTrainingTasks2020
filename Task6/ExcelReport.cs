﻿using Excel.Items;
using Microsoft.Office.Interop.Excel;
using System;
using System.Runtime.InteropServices;

namespace Excel
{
    public class ExcelReport : IDisposable
    {
        private Application app;
        private Workbook workbook;
        private Worksheet worksheet;
        public bool IsVisible
        {
            get => app.Visible;
            set => app.Visible = value;
        }

        public string WorksheetName
        {
            get => worksheet.Name;
            set => worksheet.Name = value;
        }

        public bool ExcelReportWindowVisibility
        {
            get => app.Visible;
            set => app.Visible = value;
        }

        public ExcelReport()
        {
            app = new Application();
            workbook = app.Workbooks.Add();
            worksheet = (Worksheet)workbook.ActiveSheet;
        }

        public void AddReportItem(ExcelItem item) => item.AddItem(worksheet);

        public void Save(string fileName)
        {
            app.Application.ActiveWorkbook.SaveAs(
                fileName,
                Type.Missing,
                Type.Missing,
                Type.Missing,
                Type.Missing,
                Type.Missing,
                XlSaveAsAccessMode.xlNoChange,
                XlSaveConflictResolution.xlLocalSessionChanges,
                Type.Missing,
                Type.Missing,
                Type.Missing,
                Type.Missing);
        }

        public void Dispose()
        {
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
