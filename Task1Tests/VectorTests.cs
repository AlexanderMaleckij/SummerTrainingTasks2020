using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Task1.Tests
{
    [TestClass()]
    public class VectorTests
    {
        [TestMethod()]
        public void VectorTest()
        {
            Vector vector = new Vector(0, 1, 2);
            Assert.IsNotNull(vector);
        }

        [TestMethod()]
        public void ModuleTest()
        {
            Vector vector = new Vector(-1, 3, 5);
            Assert.AreEqual(vector.Module(), Math.Sqrt(1 + 9 + 25));
        }

        [TestMethod()]
        public void ScalarMultiplicationTest()
        {
            Vector vector0 = new Vector(-1, 3, 5);
            Vector vector1 = new Vector(1, -2, 7);
            Assert.AreEqual(Vector.ScalarMultiplication(vector0, vector1), new Vector(-1 * 1, -2 * 3, 5 * 7));
        }

        [TestMethod()]
        public void VectorMultiplicationTest()
        {
            Vector vector0 = new Vector(-1, 3, 5);
            Vector vector1 = new Vector(1, -2, 7);
            Assert.AreEqual(Vector.VectorMultiplication(vector0, vector1), new Vector(31, 12, -1));
        }

        [TestMethod()]
        public void ToStringTest()
        {
            Vector vector0 = new Vector(-1, 3, 5);
            Assert.AreEqual(vector0.ToString(), "Vector -1 3 5");
        }

        [TestMethod()]
        public void GetHashCodeTest()
        {
            Vector vector0 = new Vector(-1, 3, 5);
            Vector vector1 = new Vector(-1, 3, 5);
            Vector vector2 = new Vector(3, -1, 5);

            Assert.AreEqual(vector0.GetHashCode(), vector1.GetHashCode());
            Assert.AreNotEqual(vector1.GetHashCode(), vector2.GetHashCode());
        }

        [TestMethod()]
        public void EqualsTest()
        {
            Vector vector0 = new Vector(-1, 3, 5);
            Vector vector1 = new Vector(-1, 3, 5);
            Vector vector2 = new Vector(3, -1, 5);

            Assert.AreEqual(vector0.GetHashCode(), vector1.GetHashCode());
            Assert.AreNotEqual(vector1.GetHashCode(), vector2.GetHashCode());
        }

        #region overrided operations tests

        [TestMethod()]
        public void MultiplyVectorByNumberOperationTest()
        {
            Vector vector0 = new Vector(1, -3, 5);
            Vector vector1 = vector0 * (-5);

            Assert.AreEqual(vector1, new Vector(-5, 15, -25));
        }

        [TestMethod()]
        public void DivideVectorByNumberOperationTest()
        {
            Vector vector0 = new Vector(1, -3, 5);
            Vector vector1 = vector0 / (-5);

            Assert.AreEqual(vector1, new Vector(-0.2, 3.0/5, -1));
            Assert.ThrowsException<DivideByZeroException>(() => vector0 / 0);
        }

        [TestMethod()]
        public void VectorsAdditingOperationTest()
        {
            Vector vector0 = new Vector(-1, 4, 9);
            Vector vector1 = new Vector(5, 14, -18);

            Assert.AreEqual(vector0 + vector1, new Vector(4, 18, -9));
        }

        [TestMethod()]
        public void VectorsSubtractionOperationTest()
        {
            Vector vector0 = new Vector(-1, 4, 9);
            Vector vector1 = new Vector(5, 14, -18);

            Assert.AreEqual(vector1 - vector0, new Vector(6, 10, -27));
        }

        [TestMethod()]
        public void VectorsEqualityOperationTest()
        {
            Vector vector0 = new Vector(1, -3, 5);
            Vector vector1 = new Vector(3, -9, 15);

            Assert.IsTrue(vector0 == vector1);
        }

        [TestMethod()]
        public void VectorInequalityOperationTest()
        {
            Vector vector0 = new Vector(1, -3, 5);
            Vector vector1 = new Vector(3, 9, 15);

            Assert.IsTrue(vector0 != vector1);
        }

        [TestMethod()]
        public void VectorChangeOfSignOperationTest()
        {
            Vector vector = new Vector(-1, 4, 9);
            Assert.AreEqual(-vector, new Vector(1, -4, -9));
        }
        #endregion
    }
}