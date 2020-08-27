using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Excel.Items.Tests
{
    [TestClass()]
    public class ExcelTextTests
    {
        [TestMethod()]
        public void ExcelTextTest()
        {
            ExcelText text = new ExcelText("test text");
            Assert.AreEqual("test text", text.Text);

            text.Text = "test text 2";
            Assert.AreEqual("test text 2", text.Text);
        }
    }
}