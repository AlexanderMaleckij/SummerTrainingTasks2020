using Microsoft.VisualStudio.TestTools.UnitTesting;
using Excel.Items;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Office.Interop.Excel;
using Excel.Properties;

namespace Excel.Items.Tests
{
    [TestClass()]
    public class ExcelTableTests
    {
        [TestMethod()]
        public void ExcelTableTestOneParamConstructor()
        {
            ExcelTable table = new ExcelTable(GetTestDataTable(), new ExcelItemPosition(2, 3));
            Assert.AreEqual(table.Position.CellCoordNumberX, 2);
            Assert.AreEqual(table.Position.CellCoordNumberY, 3);
            Assert.AreEqual(2, table.Size.Width);
            Assert.AreEqual(3, table.Size.Height);
        }

        [TestMethod()]
        public void ExcelTableTestTwoParamsConstructor()
        {
            ExcelTable table = new ExcelTable(GetTestDataTable());
            Assert.AreEqual(table.Position.CellCoordNumberX, 1);
            Assert.AreEqual(table.Position.CellCoordNumberY, 1);
            Assert.AreEqual(2, table.Size.Width);
            Assert.AreEqual(3, table.Size.Height);
        }

        private System.Data.DataTable GetTestDataTable()
        {
            System.Data.DataTable table = new System.Data.DataTable();
            table.Columns.Add("1st col");
            table.Columns.Add("2nd col");
            table.Rows.Add("[1;1]", "[1;2]");
            table.Rows.Add("[1;2]", "[2;2]");
            return table;
        }
    }
}