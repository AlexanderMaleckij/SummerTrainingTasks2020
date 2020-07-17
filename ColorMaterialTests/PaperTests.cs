using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ColorMaterial.Tests
{
    [TestClass()]
    public class PaperTests
    {
        [TestMethod()]
        public void PaperTest()
        {
            ColoratedMaterial coloratedMaterial = new Paper();
            Assert.IsNotNull(coloratedMaterial);
        }

        [TestMethod()]
        public void ColorizeTest()
        {
            ColoratedMaterial coloratedMaterial = new Paper();
            coloratedMaterial.Colorize(Color.Blue);
            Assert.ThrowsException<ColorationException>(() => coloratedMaterial.Colorize(Color.Green));
        }

        [TestMethod()]
        public void CloneTest()
        {
            ColoratedMaterial coloratedMaterial = new Paper();
            Assert.IsFalse(ReferenceEquals(coloratedMaterial, coloratedMaterial.Clone()));
        }

        [TestMethod()]
        public void ToStringTest()
        {
            Paper paper = new Paper();
            Assert.AreEqual(paper.ToString(), "Paper");
        }

        [TestMethod()]
        public void EqualsTest()
        {
            Paper paper0 = new Paper();
            Paper paper1 = new Paper();
            Assert.IsTrue(paper0.Equals(paper1));
        }

        [TestMethod()]
        public void GetHashCodeTest()
        {
            Paper paper0 = new Paper();
            Paper paper1 = new Paper();
            Assert.AreEqual(paper0.GetHashCode(), paper1.GetHashCode());
        }
    }
}