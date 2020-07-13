using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Task2.Tests
{
    [TestClass()]
    public class PolynomTests
    {
        [TestMethod()]
        public void PolynomTest()
        {
            Polynom polynom = new Polynom( new List<Monomial> { new Monomial(3, 2), new Monomial(0, -4) });
            Assert.IsNotNull(polynom);
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
            Polynom polynom0 = new Polynom(
               new List<Monomial> { new Monomial(2, -4), new Monomial(0, 5), new Monomial(3, -2.5) }
               ); // -4*x^2+5-2,5*x^3

            Polynom polynom1 = new Polynom(
                new List<Monomial> { new Monomial(3, 2), new Monomial(0, -4) }
                ); // 2*x^3-4

            Polynom substraction = new Polynom(
                new List<Monomial> { new Monomial(2, -4), new Monomial(0, 9), new Monomial(3, -4.5) }
                ); // -4*x^2+9-4,5*x^3

            Assert.AreEqual(polynom0 - polynom1, substraction);
        }

        [TestMethod()]
        public void PolynomsMultiplicationOperationTest()
        {
            Polynom polynom0 = new Polynom(
              new List<Monomial> { new Monomial(2, -4), new Monomial(0, 5), new Monomial(3, -2.5) }
              ); // -4*x^2+5-2,5*x^3

            Polynom polynom1 = new Polynom(
                new List<Monomial> { new Monomial(3, 2), new Monomial(0, -4) }
                ); // 2*x^3-4

            Polynom multiplication = new Polynom(
                new List<Monomial> { new Monomial(6, -5), new Monomial(5, -8), new Monomial(3, 20), new Monomial(2, 16), new Monomial(0, -20) }
                ); // -5*x^6-8*x^5+20*x*3+16*x^2-20

            Assert.AreEqual(polynom0 * polynom1, multiplication);
        }

        [TestMethod()]
        public void PolynomsDivisionOperationTest()
        {
            Polynom dividend = new Polynom(
              new List<Monomial> { new Monomial(4, 1), new Monomial(3, -2), new Monomial(1, -2), new Monomial(0, 3) }); // x^4-2*x^3-2*x+3
            Polynom divisor = new Polynom(
                new List<Monomial> { new Monomial(2, 1), new Monomial(1, -3), new Monomial(0, 1) }); //x^2-3*x+1
            Polynom resultExpected = new Polynom(
             new List<Monomial> { new Monomial(2, 1), new Monomial(1, 1), new Monomial(0, 2) }); // x^2+x+2
            Polynom remainderExpected = new Polynom(
                new List<Monomial> { new Monomial(1, 3), new Monomial(0, 1) }); //3*x+1

            (Polynom resultCalc, Polynom remainderCalc) = dividend / divisor;

            Assert.AreEqual(resultCalc, resultExpected);
            Assert.AreEqual(remainderCalc, remainderExpected);
            Assert.ThrowsException<Exception>(() => divisor / dividend);
        }

        [TestMethod()]
        public void MultiplyPolynomByNumberTest()
        {
            Polynom polynom = new Polynom(
             new List<Monomial> { new Monomial(4, 1), new Monomial(3, 3), new Monomial(0, 5) }); // x^4+3*x^3+5

            Polynom polynomExpected = new Polynom(
            new List<Monomial> { new Monomial(4, 10), new Monomial(3, 30), new Monomial(0, 50) }); // 10*x^4+30*x^3+50

            Assert.AreEqual(polynom * 10, polynomExpected);
        }

        [TestMethod()]
        public void DividePolynomByNumberTest()
        {
            Polynom polynom = new Polynom(
            new List<Monomial> { new Monomial(4, 1), new Monomial(3, 3), new Monomial(0, 5) }); // x^4+3*x^3+5

            Polynom polynomExpected = new Polynom(
            new List<Monomial> { new Monomial(4, 0.1), new Monomial(3, 0.3), new Monomial(0, 0.5) }); // 0.1*x^4+0.3*x^3+0.5

            Assert.AreEqual(polynom / 10, polynomExpected);
            Assert.ThrowsException<DivideByZeroException>(() => polynom / 0);
        }
    }
}