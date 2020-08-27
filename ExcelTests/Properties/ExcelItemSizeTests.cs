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

        [TestMethod()]
        public void EqualsTest()
        {
            ExcelItemSize size1 = new ExcelItemSize(1, 2);
            ExcelItemSize size2 = new ExcelItemSize(1, 2);
            Assert.IsTrue(size1.Equals(size2));
            Assert.IsFalse(size1.Equals(new ExcelItemSize()));
        }

        [TestMethod()]
        public void GetHashCodeTest()
        {
            ExcelItemSize size1 = new ExcelItemSize(1, 2);
            ExcelItemSize size2 = new ExcelItemSize(1, 2);
            Assert.AreEqual(size1.GetHashCode(), size2.GetHashCode());
            Assert.AreNotEqual(size1.GetHashCode(), new ExcelItemSize().GetHashCode());
        }

        [TestMethod()]
        public void ToStringTest()
        {
            ExcelItemSize size1 = new ExcelItemSize(10, 2);
            Assert.AreEqual("ExcelItemSize Width = 10; Height = 2", size1.ToString());
        }
    }
}