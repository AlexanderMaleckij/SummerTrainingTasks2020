using Xunit;
using Excel.PropertiesExceptions;

namespace Excel.Properties.Tests
{
    public class ExcelItemSizeTests
    {
        [Fact()]
        public void ExcelItemSizeParameterlessConstructorTest()
        {
            ExcelItemSize size = new ExcelItemSize();
            Assert.Equal(1, size.Width);
            Assert.Equal(1, size.Height);
        }

        [Fact()]
        public void ExcelItemSizeIntSizeParametersConstructorTest()
        {
            ExcelItemSize size = new ExcelItemSize(2, 3);
            Assert.Equal(2, size.Width);
            Assert.Equal(3, size.Height);

            Assert.Throws<ExcelItemSizeException>(() => new ExcelItemSize(0, 0));
            Assert.Throws<ExcelItemSizeException>(() => new ExcelItemSize(0, 10));
            Assert.Throws<ExcelItemSizeException>(() => new ExcelItemSize(-1, 1));
        }
    }
}