using Microsoft.VisualStudio.TestTools.UnitTesting;
using Excel.ItemsExceptions;

namespace Excel.Items.Tests
{
    [TestClass()]
    public class ExcelItemTests
    {
        [TestMethod()]
        public void PositionTest()
        {
            ExcelItem item = new ExcelText("some text");
            Assert.ThrowsException<ExcelItemException>(() => item.Position = null);
        }
    }
}