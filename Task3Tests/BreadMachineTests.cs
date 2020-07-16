using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Task3.Tests
{
    [TestClass()]
    public class BreadMachineTests
    {
        [TestMethod()]
        public void BreadMachineTest()
        {
            Assert.IsNotNull(new BreadMachine("breadMaker 2000", 150));
            Assert.ThrowsException<ArgumentException>(() => new BreadMachine("", 105.27));
            Assert.ThrowsException<ArgumentException>(() => new BreadMachine("breadMaker 2000", -105.27));
        }

        [TestMethod()]
        public void AdditionOperationTest()
        {
            BreadMachine breadMachine0 = new BreadMachine("breadMaker 2000", 150);
            BreadMachine breadMachine1 = new BreadMachine("breadMaker 3000", 350);
            BreadMachine breadMachine2 = breadMachine0 + breadMachine1;
            Assert.IsTrue(breadMachine2.Name == "breadMaker 2000 - breadMaker 3000");
            Assert.IsTrue(breadMachine2.Price == 250);
        }

        [TestMethod()]
        public void CastToScalesTest()
        {
            BreadMachine breadMachine = new BreadMachine("breadMaker 2000", 150);
            Scales scales = breadMachine;

            Assert.IsTrue(scales.Name == "breadMaker 2000");
            Assert.IsTrue(scales.Price == 150);
        }

        [TestMethod()]
        public void CastToLaptopTest()
        {
            BreadMachine breadMachine = new BreadMachine("breadMaker 2000", 150);
            Laptop laptop= breadMachine;

            Assert.IsTrue(laptop.Name == "breadMaker 2000");
            Assert.IsTrue(laptop.Price == 150);
        }

        [TestMethod()]
        public void CastToMonitoeTest()
        {
            BreadMachine breadMachine = new BreadMachine("breadMaker 2000", 150);
            Monitor monitor = breadMachine;

            Assert.IsTrue(monitor.Name == "breadMaker 2000");
            Assert.IsTrue(monitor.Price == 150);
        }
    }
}