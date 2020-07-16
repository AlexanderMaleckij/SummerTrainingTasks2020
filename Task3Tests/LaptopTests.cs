using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Task3.Tests
{
    [TestClass()]
    public class LaptopTests
    {
        [TestMethod()]
        public void LaptopTest()
        {
            Assert.IsNotNull(new Laptop("Novolen lf-4", 1350));
            Assert.ThrowsException<ArgumentException>(() => new Laptop("", 1350));
            Assert.ThrowsException<ArgumentException>(() => new Laptop("Novolen lf-4", -1350));
        }

        [TestMethod()]
        public void AdditionOperationTest()
        {
            Laptop laptop0 = new Laptop("Novolen lf-4", 1350);
            Laptop laptop1 = new Laptop("Xedp d-8", 650);
            Laptop laptop2 = laptop0 + laptop1;

            Assert.IsTrue(laptop2.Name == "Novolen lf-4 - Xedp d-8");
            Assert.IsTrue(laptop2.Price == 1000);
        }

        [TestMethod()]
        public void CastToScalesTest()
        {
            Laptop laptop = new Laptop("Novolen lf-4", 1350);
            Scales scales = (Scales)laptop;

            Assert.IsTrue(scales.Name == "Novolen lf-4");
            Assert.IsTrue(scales.Price == 1350);
        }

        [TestMethod()]
        public void CastToBreadMachineTest()
        {
            Laptop laptop = new Laptop("Novolen lf-4", 1350);
            BreadMachine breadMachine = (BreadMachine)laptop;

            Assert.IsTrue(breadMachine.Name == "Novolen lf-4");
            Assert.IsTrue(breadMachine.Price == 1350);
        }

        [TestMethod()]
        public void CastToMonitorTest()
        {
            Laptop laptop = new Laptop("Novolen lf-4", 1350);
            Monitor monitor = (Monitor)laptop;

            Assert.IsTrue(monitor.Name == "Novolen lf-4");
            Assert.IsTrue(monitor.Price == 1350);
        }
    }
}