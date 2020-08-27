using Microsoft.VisualStudio.TestTools.UnitTesting;
using Excel.Properties;
using System.Collections.Generic;
using Excel.Items;

namespace Excel.Containers.Tests
{
    [TestClass()]
    public class ExcelStackPanelTests
    {
        [TestMethod()]
        public void ExcelStackPanelVerticalTest()
        {
            //prepare data
            var text1 = new ExcelText("some test");
            var text2 = new ExcelText("text not similar to the first");
            var text3 = new ExcelText("some text too");
            text1.Size.Height = 3;
            text2.Size.Width = 4;
            //text1: width = 1; height = 3;
            //text2: width = 4; height = 1; 
            //text3: width = 1; height = 1;
            ExcelStackPanel panel = new ExcelStackPanel(Orientation.Vertical);
            panel.Items.AddRange(new List<ExcelItem> { text1, text2, text3 });
            Assert.AreEqual(4, panel.Size.Width);   //= max boxed item height (because of vertical orientation)
            Assert.AreEqual(7, panel.Size.Height);  //= 3 + 1 + 1 + 2 * default_space_between = 5 + 2 * 1 = 7
        }

        [TestMethod()]
        public void ExcelStackPanelHorizontalTest()
        {
            //prepare data
            var text1 = new ExcelText("some test");
            var text2 = new ExcelText("text not similar to the first");
            var text3 = new ExcelText("some text too");
            text1.Size.Height = 3;
            text2.Size.Width = 4;
            //text1: width = 1; height = 3;
            //text2: width = 4; height = 1; 
            //text3: width = 1; height = 1;
            ExcelStackPanel panel = new ExcelStackPanel(new List<ExcelItem>() { text1, text2}, Orientation.Horizintal);
            panel.Items.Add(text3);
            panel.SpaceBetween = 2;
            CollectionAssert.AreEqual(new List<ExcelItem> { text1, text2, text3 }, panel.Items);
            Assert.AreEqual(panel.Orientation, Orientation.Horizintal);
            Assert.AreEqual(10, panel.Size.Width);  //= 1 + 4 + 1 + 2 * space_between = 6 + 2 * 2 = 10
            Assert.AreEqual(3, panel.Size.Height);  //= max boxed item height (because of horizontal orientation)
        }
    }
}