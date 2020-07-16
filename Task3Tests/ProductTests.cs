using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Task3.Tests
{
    [TestClass()]
    public class ProductTests
    {
        [TestMethod()]
        public void ProductTest()
        {
            Assert.IsNotNull(new BreadMachine("vesta 100", 105.27));
            Assert.ThrowsException<ArgumentException>(() => new BreadMachine("", 105.27));
            Assert.ThrowsException<ArgumentException>(() => new BreadMachine("vesta 100", -105.27));
        }

        [TestMethod()]
        public void ProductToDoubleTest()
        {
            Assert.AreEqual((double)new BreadMachine("vesta 100", 105.27), 105.27);
        }

        [TestMethod()]
        public void ProductToIntTest()
        {
            Assert.AreEqual((int)new BreadMachine("vesta 100", 105.27), 10527);
        }
    }
}