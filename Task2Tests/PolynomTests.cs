using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task2;
using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace Task2.Tests
{
    [TestClass()]
    public class PolynomTests
    {
        [TestMethod()]
        public void PolynomTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void PolynomsAdditingOperationTest()
        {
            Polynom polynom0 = new Polynom(
                new List<Monomial> { new Monomial(2, -4), new Monomial(0, 5), new Monomial(3, -2.5) }
                ); // -4*x^2+5-2,5*x^3

            Polynom polynom1 = new Polynom(
                new List<Monomial> { new Monomial(3, 2), new Monomial(0, -4) }
                ); // 2*x^3-4

            Polynom summ = new Polynom(
                new List<Monomial> { new Monomial(2, -4), new Monomial(0, 1), new Monomial(3, -0.5) }
                ); // -4*x^2+1-0,5*x^3


            Debug.Print(polynom0.ToString());
            Debug.Print(polynom1.ToString());
            Debug.Print(summ.ToString());

            Assert.AreEqual(polynom0 + polynom1, summ);
        }

        [TestMethod()]
        public void PolynomsSubtractionOperationTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void PolynomsMultiplicationOperationTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void PolynomsDivisionOperationTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void MultiplyPolynomByNumberTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void DividePolynomByNumberTest()
        {
            Assert.Fail();
        }
    }
}