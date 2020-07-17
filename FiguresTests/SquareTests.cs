using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Drawing;

namespace Figures.Tests
{
    [TestClass()]
    public class SquareTests
    {
        [TestMethod()]
        public void SquareCreateByLinesTest()
        {
            Square triangle0 = new Square(3);
            Assert.IsNotNull(triangle0);

            var ex = Assert.ThrowsException<ArgumentException>(() => new Square(-1));
            Assert.AreEqual(ex.Message, "length of the side can't be negative");
        }

        [TestMethod()]
        public void SquareCreateByPointsTest()
        {
            Square triangle0 = new Square(new PointF(0, 0), new PointF(0, 1), new PointF(1, 0), new PointF(1, 1));
            Assert.IsNotNull(triangle0);

            var ex = Assert.ThrowsException<Exception>(() => new Square(new PointF(0, -1), new PointF(0, 1), new PointF(1, 0), new PointF(1, 1)));
            Assert.AreEqual(ex.Message, "Points don't form a square!");

            ex = Assert.ThrowsException<Exception>(() => new Square(new PointF(0, 0), new PointF(0, 1), new PointF(5, 0), new PointF(5, 1)));
            Assert.AreEqual(ex.Message, "Points don't form a square!");
        }

        [TestMethod()]
        public void CreateSquareFromAnotherFigureTest()
        {
            Figure rectangle = new Rectangle(5, 2);
            Assert.IsNotNull(new Square(3.1, rectangle));
            Assert.ThrowsException<CuttingException>(() => new Square(3.2, rectangle));
        }

        [TestMethod()]
        public void AreaTest()
        {
            Square square0 = new Square(new PointF(0, 0), new PointF(0, 1), new PointF(1, 0), new PointF(1, 1));
            Assert.AreEqual(square0.Area(), 1);

            Square square1 = new Square(4);
            Assert.AreEqual(square1.Area(), 16);
        }

        [TestMethod()]
        public void PerimeterTest()
        {
            Square square0 = new Square(new PointF(0, 0), new PointF(0, 1), new PointF(1, 0), new PointF(1, 1));
            Assert.AreEqual(square0.Perimeter(), 4);

            Square square1 = new Square(5);
            Assert.AreEqual(square1.Perimeter(), 20);
        }

        [TestMethod()]
        public void ToStringTest()
        {
            Square square0 = new Square(new PointF(0, 0), new PointF(0, 1), new PointF(1, 0), new PointF(1, 1));
            Assert.AreEqual(square0.ToString(), "Square 1");

            Square square1 = new Square(5);
            Assert.AreEqual(square1.ToString(), "Square 5");
        }

        [TestMethod()]
        public void GetHashCodeTest()
        {
            Square square0 = new Square(new PointF(0, 0), new PointF(0, 1), new PointF(1, 0), new PointF(1, 1));
            Square square1 = new Square(new PointF(0, 0), new PointF(0, 1), new PointF(1, 0), new PointF(1, 1));
            Square square2 = new Square(new PointF(0, 0), new PointF(0, 2), new PointF(2, 0), new PointF(2, 2));

            Assert.AreEqual(square0.GetHashCode(), square1.GetHashCode());
            Assert.AreNotEqual(square1.GetHashCode(), square2.GetHashCode());

            Square square3 = new Square(2);
            Square square4 = new Square(2);
            Square square5 = new Square(3);

            Assert.AreEqual(square3.GetHashCode(), square4.GetHashCode());
            Assert.AreNotEqual(square4.GetHashCode(), square5.GetHashCode());
        }

        [TestMethod()]
        public void EqualsTest()
        {
            Square square0 = new Square(new PointF(0, 0), new PointF(0, 1), new PointF(1, 0), new PointF(1, 1));
            Square square1 = new Square(new PointF(0, 0), new PointF(0, 1), new PointF(1, 0), new PointF(1, 1));
            Square square2 = new Square(new PointF(0, 0), new PointF(0, 2), new PointF(2, 0), new PointF(2, 2));

            Assert.IsTrue(square0.Equals(square1));
            Assert.IsFalse(square1.Equals(square2));

            Square square3 = new Square(2);
            Square square4 = new Square(2);
            Square square5 = new Square(3);

            Assert.IsTrue(square3.Equals(square4));
            Assert.IsFalse(square4.Equals(square5));
        }
    }
}