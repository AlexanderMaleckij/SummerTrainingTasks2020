using Microsoft.VisualStudio.TestTools.UnitTesting;
using Excel.Properties;
using System;
using System.Collections.Generic;
using System.Text;
using Excel.PropertiesExceptions;

namespace Excel.Properties.Tests
{
    [TestClass()]
    public class ExcelItemPositionTests
    {
        [TestMethod()]
        public void ExcelItemPositionParameterlessConstructorTest()
        {
            ExcelItemPosition position = new ExcelItemPosition();
            Assert.AreEqual(1, position.CellCoordNumberX);
            Assert.AreEqual(1, position.CellCoordNumberY);
            Assert.AreEqual("A", position.CellCoordX);
            Assert.AreEqual("A", position.CellCoordY);
        }

        [TestMethod()]
        public void ExcelItemPositionStringCoordsConstructorTest()
        {
            ExcelItemPosition position = new ExcelItemPosition("B", "C");
            Assert.AreEqual(2, position.CellCoordNumberX);
            Assert.AreEqual(3, position.CellCoordNumberY);
            Assert.AreEqual("B", position.CellCoordX);
            Assert.AreEqual("C", position.CellCoordY);

            Assert.ThrowsException<ExcelItemPositionException>(() => new ExcelItemPosition("21B", "C"));
            Assert.ThrowsException<ExcelItemPositionException>(() => new ExcelItemPosition("B", "21C"));
        }

        [TestMethod()]
        public void ExcelItemPositionIntCoordsConstructorTest()
        {
            ExcelItemPosition position = new ExcelItemPosition(2, 3);
            Assert.AreEqual(2, position.CellCoordNumberX);
            Assert.AreEqual(3, position.CellCoordNumberY);
            Assert.AreEqual("B", position.CellCoordX);
            Assert.AreEqual("C", position.CellCoordY);

            Assert.ThrowsException<ExcelItemPositionException>(() => new ExcelItemPosition(1, 0));
            Assert.ThrowsException<ExcelItemPositionException>(() => new ExcelItemPosition(-1, 10));
        }
    }
}