using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Figures.Tests
{
    [TestClass()]
    public class CircleTests
    {
        [TestMethod()]
        public void CircleCreateByRadiusTest()
        {
            Figure circle0 = new Circle(3);
            Assert.IsNotNull(circle0);

            var ex = Assert.ThrowsException<ArgumentException>(() => new Circle(-4));
            Assert.AreEqual(ex.Message, "length of the side can't be negative");
        }

        [TestMethod()]
        public void AreaTest()
        {
            Figure circle0 = new Circle(3);
            Assert.AreEqual(circle0.Area(), Math.PI * 9);
        }

        [TestMethod()]
        public void PerimeterTest()
        {
            Figure circle0 = new Circle(3);
            Assert.AreEqual(circle0.Perimeter(), 2 * Math.PI * 3);
        }

        [TestMethod()]
        public void ToStringTest()
        {
            Figure circle0 = new Circle(3);
            Assert.AreEqual(circle0.ToString(), "Circle 3");
        }

        [TestMethod()]
        public void GetHashCodeTest()
        {
            Figure circle0 = new Circle(3);
            Figure circle1 = new Circle(3);
            Figure circle2 = new Circle(1);

            Assert.AreEqual(circle0.GetHashCode(), circle1.GetHashCode());
            Assert.AreNotEqual(circle1.GetHashCode(), circle2.GetHashCode());
        }

        [TestMethod()]
        public void EqualsTest()
        {
            Figure circle0 = new Circle(3);
            Figure circle1 = new Circle(3);
            Figure circle2 = new Circle(1);

            Assert.IsTrue(circle0.Equals(circle1));
            Assert.IsFalse(circle1.Equals(circle2));
        }
    }
}
