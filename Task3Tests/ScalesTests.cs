using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Task3.Tests
{
    [TestClass()]
    public class ScalesTests
    {
        [TestMethod()]
        public void ScalesTest()
        {
            Assert.IsNotNull(new Scales("Fetal a2", 75));
            Assert.ThrowsException<ArgumentException>(() => new Scales("", 75));
            Assert.ThrowsException<ArgumentException>(() => new Scales("Fetal a2", -75));
        }

        [TestMethod()]
        public void AdditionOperationTest()
        {
            Scales scales0 = new Scales("Fetal a2", 75);
            Scales scales1 = new Scales("Fetal a3", 85);
            Scales scales2 = scales0 + scales1;

            Assert.IsTrue(scales2.Name == "Fetal a2 - Fetal a3");
            Assert.IsTrue(scales2.Price == 80);
        }

        [TestMethod()]
        public void CastToMonitorTest()
        {
            Scales scales = new Scales("Fetal a2", 75);
            Monitor monitor = (Monitor)scales;

            Assert.IsTrue(monitor.Name == "Fetal a2");
            Assert.IsTrue(monitor.Price == 75);
        }

        [TestMethod()]
        public void CastToBreadMachineTest()
        {
            Scales scales = new Scales("Fetal a2", 75); ;
            BreadMachine breadMachine = (BreadMachine)scales;

            Assert.IsTrue(breadMachine.Name == "Fetal a2");
            Assert.IsTrue(breadMachine.Price == 75);
        }

        [TestMethod()]
        public void CastToLaptopTest()
        {
            Scales scales = new Scales("Fetal a2", 75);
            Laptop laptop = (Laptop)scales;

            Assert.IsTrue(laptop.Name == "Fetal a2");
            Assert.IsTrue(laptop.Price == 75);
        }
    }
}