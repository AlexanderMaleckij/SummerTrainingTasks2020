using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ColorMaterial.Tests
{
    [TestClass()]
    public class FilmTests
    {
        [TestMethod()]
        public void ColorizeTest()
        {
            ColoratedMaterial coloratedMaterial = new Film();
            Assert.ThrowsException<ColorationException>(() => coloratedMaterial.Colorize(Color.Blue));
        }

        [TestMethod()]
        public void CloneTest()
        {
            ColoratedMaterial coloratedMaterial = new Film();
            Assert.IsFalse(ReferenceEquals(coloratedMaterial, coloratedMaterial.Clone()));
        }

        [TestMethod()]
        public void ToStringTest()
        {
            Film film = new Film();
            Assert.AreEqual(film.ToString(), "Film");
        }

        [TestMethod()]
        public void EqualsTest()
        {
            Film film0 = new Film();
            Film film1 = new Film();
            Assert.IsTrue(film0.Equals(film1));
        }

        [TestMethod()]
        public void GetHashCodeTest()
        {
            Film film0 = new Film();
            Film film1 = new Film();
            Assert.AreEqual(film0.GetHashCode(), film1.GetHashCode());
        }
    }
}