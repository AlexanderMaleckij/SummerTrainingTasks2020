using Microsoft.VisualStudio.TestTools.UnitTesting;
using Excel.PropertiesExceptions;

namespace Excel.Properties.Tests
{
    [TestClass()]
    public class ExcelItemSizeTests
    {
        [TestMethod()]
        public void ExcelItemSizeParameterlessConstructorTest()
        {
            ExcelItemSize size = new ExcelItemSize();
            Assert.AreEqual(1, size.Width);
            Assert.AreEqual(1, size.Height);
        }

        [TestMethod()]
        public void ExcelItemSizeIntSizeParametersConstructorTest()
        {
            ExcelItemSize size = new ExcelItemSize(2, 3);
            Assert.AreEqual(2, size.Width);
            Assert.AreEqual(3, size.Height);

            Assert.ThrowsException<ExcelItemSizeException>(() => new ExcelItemSize(0, 0));
            Assert.ThrowsException<ExcelItemSizeException>(() => new ExcelItemSize(0, 10));
            Assert.ThrowsException<ExcelItemSizeException>(() => new ExcelItemSize(-1, 1));
        }
    }
}