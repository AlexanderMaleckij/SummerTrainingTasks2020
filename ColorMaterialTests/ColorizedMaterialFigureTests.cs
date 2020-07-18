using Microsoft.VisualStudio.TestTools.UnitTesting;
using Figures;

namespace ColorMaterial.Tests
{
    [TestClass()]
    public class ColorizedMaterialFigureTests
    {
        [TestMethod()]
        public void ColorizedMaterialFigureTest()
        {
            ColorizedMaterialFigure colorizedMaterialFigure = new ColorizedMaterialFigure(
                new Rectangle(2, 5), 
                new Film());
            Assert.IsNotNull(colorizedMaterialFigure);
        }

        [TestMethod()]
        public void CloneTest()
        {
            ColorizedMaterialFigure colorizedMaterialFigure = new ColorizedMaterialFigure(
               new Rectangle(2, 5),
               new Film());
            Assert.IsFalse(ReferenceEquals(colorizedMaterialFigure, colorizedMaterialFigure.Clone()));
        }

        [TestMethod()]
        public void EqualsTest()
        {
            ColorizedMaterialFigure colorizedMaterialFigure0 = new ColorizedMaterialFigure(
              new Rectangle(2, 5),
              new Film());
            ColorizedMaterialFigure colorizedMaterialFigure1 = new ColorizedMaterialFigure(
              new Rectangle(2, 5),
              new Film());
            ColorizedMaterialFigure colorizedMaterialFigure2 = new ColorizedMaterialFigure(
             new Rectangle(2, 4),
             new Film());

            Assert.AreEqual(colorizedMaterialFigure0, colorizedMaterialFigure1);
            Assert.AreNotEqual(colorizedMaterialFigure0, colorizedMaterialFigure2);
        }

        [TestMethod()]
        public void GetHashCodeTest()
        {
            ColorizedMaterialFigure colorizedMaterialFigure0 = new ColorizedMaterialFigure(
                new Rectangle(2, 5),
                new Film());
            ColorizedMaterialFigure colorizedMaterialFigure1 = new ColorizedMaterialFigure(
                new Rectangle(2, 5),
                new Paper());
            ColorizedMaterialFigure colorizedMaterialFigure2 = new ColorizedMaterialFigure(
                new Square(3),
                new Film());
            ColorizedMaterialFigure colorizedMaterialFigure3 = new ColorizedMaterialFigure(
                new Rectangle(2, 5),
                new Film());

            Assert.AreNotEqual(colorizedMaterialFigure0, colorizedMaterialFigure1);
            Assert.AreNotEqual(colorizedMaterialFigure0, colorizedMaterialFigure2);
            Assert.AreEqual(colorizedMaterialFigure0, colorizedMaterialFigure3);
        }

        [TestMethod()]
        public void ToStringTest()
        {
            ColorizedMaterialFigure colorizedMaterialFigure0 = new ColorizedMaterialFigure(
                new Rectangle(2, 5),
                new Film());
            ColorizedMaterialFigure colorizedMaterialFigure1 = new ColorizedMaterialFigure(
                new Triangle(3, 4, 5),
                new Paper());
            Assert.IsTrue(colorizedMaterialFigure0.ToString() == "Rectangle 2 5 Transparent Film");
            Assert.IsTrue(colorizedMaterialFigure1.ToString() == "Triangle 3 4 5 White Paper");
        }
    }
}