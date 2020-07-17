using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Drawing;

namespace Figures.Tests
{
    [TestClass()]
    public class TriangleTests
    {
        const string negativeSideLengthMsg = "length of the side can't be negative";

        [TestMethod()]
        public void TriangleCreateByLinesTest()
        {
            Triangle triangle0 = new Triangle(3, 4, 5);
            Assert.IsNotNull(triangle0);

            var ex = Assert.ThrowsException<ArgumentException>(() => new Triangle(0, 1, 2));
            Assert.AreEqual(ex.Message, negativeSideLengthMsg);

            ex = Assert.ThrowsException<ArgumentException>(() => new Triangle(5, -1, 2));
            Assert.AreEqual(ex.Message, negativeSideLengthMsg);

            ex = Assert.ThrowsException<ArgumentException>(() => new Triangle(5, 6, -2));
            Assert.AreEqual(ex.Message, negativeSideLengthMsg);
        }

        [TestMethod()]
        public void TriangleCreateByPointsTest()
        {
            var ex = Assert.ThrowsException<Exception>(() => new Triangle(new PointF(0, 0), new PointF(1, 1), new PointF(2, 2)));
            Assert.AreEqual(ex.Message, "Triangle points can't lie on one line!");

            Triangle triangle0 = new Triangle(new PointF(0, 0), new PointF(1, 1), new PointF(2, -1));
            Assert.IsNotNull(triangle0);
        }

        [TestMethod()]
        public void CreateTriangleFromAnotherFigureTest()
        {
            Figure rectangle = new Rectangle(5, 2);
            Assert.IsNotNull(new Triangle(2, 2, 2, rectangle));
            Assert.ThrowsException<CuttingException>(() => new Triangle(5, 5, 5, rectangle));
        }

        [TestMethod()]
        public void AreaTest()
        {
            Triangle triangle = new Triangle(3, 4, 5);
            Assert.AreEqual(triangle.Area(), 6);
        }

        [TestMethod()]
        public void PerimeterTest()
        {
            Triangle triangle = new Triangle(3, 4, 5);
            Assert.AreEqual(triangle.Perimeter(), 12);
        }

        [TestMethod()]
        public void ToStringTest()
        {
            Triangle triangle = new Triangle(3, 4, 5);
            Assert.AreEqual(triangle.ToString(), "Triangle 3 4 5");
        }

        [TestMethod()]
        public void GetHashCodeTest()
        {
            Triangle triangle1 = new Triangle(3, 4, 5);
            Triangle triangle2 = new Triangle(3, 4, 5);
            Triangle triangle3 = new Triangle(5, 4, 3);
            Assert.AreEqual(triangle1.GetHashCode(), triangle2.GetHashCode());
            Assert.AreNotEqual(triangle1.GetHashCode(), triangle3.GetHashCode());
        }

        [TestMethod()]
        public void EqualsTest()
        {
            Triangle triangle1 = new Triangle(3, 4, 5);
            Triangle triangle2 = new Triangle(3, 4, 5);
            Triangle triangle3 = new Triangle(5, 4, 3);
            Assert.IsTrue(triangle1.Equals(triangle2));
            Assert.IsFalse(triangle1.Equals(triangle3));
        }
    }
}