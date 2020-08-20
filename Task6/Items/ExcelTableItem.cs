using Excel.ItemsProperties;
using Microsoft.Office.Interop.Excel;
using System;
using System.Data;

namespace Excel.Items
{
    public class ExcelTableItem : ExcelItem
    {
        public System.Data.DataTable Table
        {
            get => Table;
            set
            {
                if (value == null)
                {
                    throw new ExcelItemException("Table can't be null");
                }

                Table = value;
            }
        }

        public ExcelTableItem(System.Data.DataTable table)
        {
            this.Table = table;
            Position = new ExcelItemPosition();
        }

        public ExcelTableItem(System.Data.DataTable table, ExcelItemPosition position)
        {
            Table = table;
            Position = position;
        }

        public override void AddItem(Worksheet worksheet)
        {
            MarkupTablePlace(worksheet);
            FillTableHeader(worksheet);
            FillTableBody(worksheet);
        }

        private void MarkupTablePlace(Worksheet worksheet)
        {
            Range leftUpCorner = (Range)worksheet.Cells[Position.CellCoordNumberX,
                                                        Position.CellCoordNumberY];

            Range rightDownCorner = (Range)worksheet.Cells[Position.CellCoordNumberX + Table.Columns.Count,
                                                           Position.CellCoordNumberY + Table.Rows.Count + 1];

            Range tableRange = worksheet.get_Range(leftUpCorner, rightDownCorner);

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
                worksheet.Cells[currentCellCoordX, Position.CellCoordNumberY] = column.ColumnName;
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
                    worksheet.Cells[currentCellCoordX, currentCellCoordY] = cell;
                    currentCellCoordX++;
                }

                currentCellCoordY++;
            }
        }
    }
}
