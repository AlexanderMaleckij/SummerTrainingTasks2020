using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Task1;

namespace Task1Tests
{
    [TestClass]
    public class GCDTests
    {
        [TestMethod]
        public void EuclidGCDOfTwoNumbersTest()
        {
            Assert.AreEqual(GCD.EuclidGCD(0, 0, out _), 0);
            Assert.AreEqual(GCD.EuclidGCD(0, 8888, out _), 8888);
            Assert.AreEqual(GCD.EuclidGCD(8888, 0, out _), 8888);
            Assert.AreEqual(GCD.EuclidGCD(8888, 8888, out _), 8888);

            Assert.AreEqual(GCD.EuclidGCD(4, 18, out _), 2);
        }

        [TestMethod]
        public void EuclidGCDOfThreeNumbersTest()
        {
            Assert.AreEqual(GCD.EuclidGCD(0, 0, 0), 0);
            Assert.AreEqual(GCD.EuclidGCD(0, 8888, 0), 8888);
            Assert.AreEqual(GCD.EuclidGCD(8888, 0, 8888), 8888);
            Assert.AreEqual(GCD.EuclidGCD(8888, 8888, 8888), 8888);

            Assert.AreEqual(GCD.EuclidGCD(4, 18, 8), 2);
        }

        [TestMethod]
        public void EuclidGCDOfFourNumbersTest()
        {
            Assert.AreEqual(GCD.EuclidGCD(0, 0, 0, 0), 0);
            Assert.AreEqual(GCD.EuclidGCD(0, 8888, 0, 0), 8888);
            Assert.AreEqual(GCD.EuclidGCD(8888, 0, 8888, 8888), 8888);
            Assert.AreEqual(GCD.EuclidGCD(8888, 8888, 8888, 8888), 8888);

            Assert.AreEqual(GCD.EuclidGCD(4, 18, 8, 64), 2);
            Assert.AreEqual(GCD.EuclidGCD(4, 18, 8, 51), 1);
        }

        [TestMethod]
        public void SteinGCDTest()
        {
            Assert.AreEqual(GCD.EuclidGCD(0, 0, out _), 0);
            Assert.AreEqual(GCD.EuclidGCD(0, 8888, out _), 8888);
            Assert.AreEqual(GCD.EuclidGCD(8888, 0, out _), 8888);
            Assert.AreEqual(GCD.EuclidGCD(8888, 8888, out _), 8888);

            Assert.AreEqual(GCD.EuclidGCD(4, 18, out _), 2);
        }

        [TestMethod]
        public void PrepareHistogramDataTestTest()
        {
            Dictionary <string, long> measurements = GCD.PrepareHistogramData(543888888, 964887888);

            Assert.IsTrue(measurements.ContainsKey("Euclid GCD time"));
            Assert.IsTrue(measurements.ContainsKey("Stein GCD time"));

            Assert.IsTrue(measurements["Euclid GCD time"] >= measurements["Stein GCD time"]);
        }
    }
}
