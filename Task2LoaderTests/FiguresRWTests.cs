using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task2Loader;
using Task2Figures;

namespace Task2Loader.Tests
{
    [TestClass()]
    public class FiguresRWTests
    {
        [TestMethod()]
        public void FiguresRWTest()
        {
            FiguresRW figuresRW = new FiguresRW("figures.txt");
            Assert.IsNotNull(figuresRW);
        }

        [TestMethod()]
        public void ReadAndWriteTest()
        {
            Figure circle = new Circle(3);
            Figure rectangle = new Rectangle(2, 4);
            Figure square = new Square(5);
            Figure triangle = new Triangle(2, 3, 4);

            Figure[] figuresForWriting = { circle, rectangle, square, triangle};

            FiguresRW figuresRW = new FiguresRW("figures.txt");
            figuresRW.Write(figuresForWriting);
            Figure[] readFigures = figuresRW.Read();

            for(int i = 0; i < figuresForWriting.Length; i++)
            {
                Assert.IsTrue(figuresForWriting[i].Equals(readFigures[i]));
            }
        }
    }
}