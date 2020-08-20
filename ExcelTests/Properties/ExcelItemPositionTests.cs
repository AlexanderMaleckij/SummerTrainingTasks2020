using Xunit;
using Excel.PropertiesExceptions;

namespace Excel.Properties.Tests
{
    public class ExcelItemPositionTests
    {
        [Fact()]
        public void ExcelItemPositionParameterlessConstructorTest()
        {
            ExcelItemPosition position = new ExcelItemPosition();
            Assert.Equal(1, position.CellCoordNumberX);
            Assert.Equal(1, position.CellCoordNumberY);
            Assert.Equal("A", position.CellCoordX);
            Assert.Equal("A", position.CellCoordY);
        }

        [Fact()]
        public void ExcelItemPositionStringCoordsConstructorTest()
        {
            ExcelItemPosition position = new ExcelItemPosition("B", "C");
            Assert.Equal(2, position.CellCoordNumberX);
            Assert.Equal(3, position.CellCoordNumberY);
            Assert.Equal("B", position.CellCoordX);
            Assert.Equal("C", position.CellCoordY);

            Assert.Throws<ExcelItemPositionException>(() => new ExcelItemPosition("21B", "C"));
            Assert.Throws<ExcelItemPositionException>(() => new ExcelItemPosition("B", "21C"));
        }

        [Fact()]
        public void ExcelItemPositionIntCoordsConstructorTest()
        {
            ExcelItemPosition position = new ExcelItemPosition(2, 3);
            Assert.Equal(2, position.CellCoordNumberX);
            Assert.Equal(3, position.CellCoordNumberY);
            Assert.Equal("B", position.CellCoordX);
            Assert.Equal("C", position.CellCoordY);

            Assert.Throws<ExcelItemPositionException>(() => new ExcelItemPosition(1, 0));
            Assert.Throws<ExcelItemPositionException>(() => new ExcelItemPosition(-1, 10));
        }
    }
}