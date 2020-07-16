using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Task3.Tests
{
    [TestClass()]
    public class MonitorTests
    {
        [TestMethod()]
        public void MonitorTest()
        {
            Assert.IsNotNull(new Monitor("Gl a2", 130));
            Assert.ThrowsException<ArgumentException>(() => new Monitor("", 130));
            Assert.ThrowsException<ArgumentException>(() => new Monitor("Gl a2", -1350));
        }

        [TestMethod()]
        public void AdditionOperationTest()
        {
            Monitor monitor0 = new Monitor("Gl a2", 130);
            Monitor monitor1 = new Monitor("Gl a3", 150);
            Monitor monitor2 = monitor0 + monitor1;

            Assert.IsTrue(monitor2.Name == "Gl a2 - Gl a3");
            Assert.IsTrue(monitor2.Price == 140);
        }

        [TestMethod()]
        public void CastToScalesTest()
        {
            Monitor monitor = new Monitor("Gl a2", 130);
            Scales scales = (Scales)monitor;

            Assert.IsTrue(scales.Name == "Gl a2");
            Assert.IsTrue(scales.Price == 130);
        }

        [TestMethod()]
        public void CastToBreadMachineTest()
        {
            Monitor monitor = new Monitor("Gl a2", 130);
            BreadMachine breadMachine = (BreadMachine)monitor;

            Assert.IsTrue(breadMachine.Name == "Gl a2");
            Assert.IsTrue(breadMachine.Price == 130);
        }

        [TestMethod()]
        public void CastToLaptopTest()
        {
            Monitor monitor = new Monitor("Gl a2", 130);
            Laptop laptop = (Laptop)monitor;

            Assert.IsTrue(laptop.Name == "Gl a2");
            Assert.IsTrue(laptop.Price == 130);
        }
    }
}