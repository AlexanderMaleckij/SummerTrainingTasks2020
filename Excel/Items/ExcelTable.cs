using Excel.ItemsExceptions;
using Excel.Properties;
using Microsoft.Office.Interop.Excel;
using System;
using System.Data;
using Range = Microsoft.Office.Interop.Excel.Range;

namespace Excel.Items
{
    /// <summary>
    /// Class that represents excel table without header
    /// </summary>
    public class ExcelTable : ExcelItem
    {
        private System.Data.DataTable table;

        public System.Data.DataTable Table
        {
            get => table;
            set
            {
                if (value == null)
                {
                    throw new ExcelTableException("Table can't be null");
                }

                table = value;
            }
        }
        public override ExcelItemSize Size
        {
            get
            {
                return new ExcelItemSize(
                    Table.Columns.Count,
                    Table.Rows.Count + 1);
            }
            set { }
        }


        public ExcelTable(System.Data.DataTable table)
        {
            Table = table;
            Position = new ExcelItemPosition();
        }

        public ExcelTable(System.Data.DataTable table, ExcelItemPosition position)
        {
            Table = table;
            Position = position;
        }

        internal override void AddItem(Worksheet worksheet)
        {
            Range tableRange = GetTableRange(worksheet);
            MarkupTablePlace(worksheet, tableRange);
            FillTableHeader(worksheet);
            FillTableBody(worksheet);
            SetAutoFitTableRange(tableRange);
        }

        private Range GetTableRange(Worksheet worksheet)
        {
            Range leftUpCorner = (Range)worksheet.Cells[Position.CellCoordNumberY,
                                                        Position.CellCoordNumberX];

            Range rightDownCorner = (Range)worksheet.Cells[Position.CellCoordNumberY + Table.Rows.Count,
                                                           Position.CellCoordNumberX + Table.Columns.Count - 1];

            Range tableRange = worksheet.get_Range(leftUpCorner, rightDownCorner);

            return tableRange;
        }

        private void MarkupTablePlace(Worksheet worksheet, Range tableRange)
        {
            worksheet.ListObjects.Add(
                XlListObjectSourceType.xlSrcRange,
                tableRange,
                Type.Missing,
                XlYesNoGuess.xlYes);
        }

        private void FillTableHeader(Worksheet worksheet)
        {
            int currentCellCoordX = Position.CellCoordNumberX;

            foreach (DataColumn column in Table.Columns)
            {
                worksheet.Cells[Position.CellCoordNumberY, currentCellCoordX] = column.ColumnName;
                currentCellCoordX++;
            }
        }

        private void FillTableBody(Worksheet worksheet)
        {
            int xStart = Position.CellCoordNumberX;
            int currentCellCoordY = Position.CellCoordNumberY + 1;

            foreach (DataRow row in Table.Rows)
            {
                var cells = row.ItemArray;
                int currentCellCoordX = xStart;

                foreach (object cell in cells)
                {
                    worksheet.Cells[currentCellCoordY, currentCellCoordX] = cell;
                    currentCellCoordX++;
                }

                currentCellCoordY++;
            }
        }

        private void SetAutoFitTableRange(Range tableRange)
        {
            tableRange.EntireColumn.AutoFit();
            tableRange.EntireRow.AutoFit();
        }
    }
}
