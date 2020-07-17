using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Drawing;

namespace Figures.Tests
{
    [TestClass()]
    public class RectangleTests
    {
        [TestMethod()]
        public void RectangleCreateByLinesTest()
        {
            Figure rectangle0 = new Rectangle(3, 4);
            Assert.IsNotNull(rectangle0);

            var ex = Assert.ThrowsException<ArgumentException>(() => new Rectangle(3, -4));
            Assert.AreEqual(ex.Message, "length of the side can't be negative");
        }

        [TestMethod()]
        public void RectangleCreateByPointsTest()
        {
            Figure rectangle0 = new Rectangle(new PointF(0, 0), new PointF(0, 5), new PointF(1, 0), new PointF(1, 5));
            Assert.IsNotNull(rectangle0);

            var ex = Assert.ThrowsException<Exception>(() => new Rectangle(new PointF(0, -7), new PointF(0, 1), new PointF(1, 0), new PointF(1, 1)));
            Assert.AreEqual(ex.Message, "Points don't form a rectangle!");
        }

        [TestMethod()]
        public void CreateRectangleFromAnotherFigureTest()
        {
            Square square = new Square(5);
            Assert.IsNotNull(new Rectangle(5, 5, square));
            Assert.ThrowsException<CuttingException>(() => new Rectangle(5, 6, square));
        }

        [TestMethod()]
        public void AreaTest()
        {
            Figure rectangle0 = new Rectangle(new PointF(0, 0), new PointF(0, 5), new PointF(1, 0), new PointF(1, 5));
            Assert.AreEqual(rectangle0.Area(), 5);

            Figure rectangle1 = new Rectangle(4, 5);
            Assert.AreEqual(rectangle1.Area(), 20);
        }

        [TestMethod()]
        public void PerimeterTest()
        {
            Figure rectangle0 = new Rectangle(new PointF(0, 0), new PointF(0, 5), new PointF(1, 0), new PointF(1, 5));
            Assert.AreEqual(rectangle0.Perimeter(), 12);

            Figure rectangle1 = new Rectangle(4, 5);
            Assert.AreEqual(rectangle1.Perimeter(), 18);
        }

        [TestMethod()]
        public void ToStringTest()
        {
            Figure rectangle0 = new Rectangle(new PointF(0, 0), new PointF(0, 5), new PointF(1, 0), new PointF(1, 5));
            Assert.AreEqual(rectangle0.ToString(), "Rectangle 5 1");

            Figure rectangle1 = new Rectangle(4, 5);
            Assert.AreEqual(rectangle1.ToString(), "Rectangle 4 5");
        }

        [TestMethod()]
        public void GetHashCodeTest()
        {
            Figure rectangle0 = new Rectangle(new PointF(0, 0), new PointF(0, 5), new PointF(1, 0), new PointF(1, 5));
            Figure rectangle1 = new Rectangle(new PointF(0, 0), new PointF(0, 5), new PointF(1, 0), new PointF(1, 5));
            Figure rectangle2 = new Rectangle(new PointF(0, 0), new PointF(0, 1), new PointF(5, 0), new PointF(5, 1));

            Assert.AreEqual(rectangle0.GetHashCode(), rectangle1.GetHashCode());
            Assert.AreNotEqual(rectangle1.GetHashCode(), rectangle2.GetHashCode());

            Figure rectangle3 = new Rectangle(2, 3);
            Figure rectangle4 = new Rectangle(2, 3);
            Figure rectangle5 = new Rectangle(3, 2);

            Assert.AreEqual(rectangle3.GetHashCode(), rectangle4.GetHashCode());
            Assert.AreNotEqual(rectangle4.GetHashCode(), rectangle5.GetHashCode());
        }

        [TestMethod()]
        public void EqualsTest()
        {
            Figure rectangle0 = new Rectangle(new PointF(0, 0), new PointF(0, 5), new PointF(1, 0), new PointF(1, 5));
            Figure rectangle1 = new Rectangle(new PointF(0, 0), new PointF(0, 5), new PointF(1, 0), new PointF(1, 5));
            Figure rectangle2 = new Rectangle(new PointF(0, 0), new PointF(0, 1), new PointF(5, 0), new PointF(5, 1));

            Assert.IsTrue(rectangle0.Equals(rectangle1));
            Assert.IsFalse(rectangle1.Equals(rectangle2));

            Figure rectangle3 = new Rectangle(2, 3);
            Figure rectangle4 = new Rectangle(2, 3);
            Figure rectangle5 = new Rectangle(3, 2);

            Assert.IsTrue(rectangle3.Equals(rectangle4));
            Assert.IsFalse(rectangle4.Equals(rectangle5));
        }
    }
}