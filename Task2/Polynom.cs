using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Task2
{
    /// <summary>
    /// Class for working with polynomials
    /// that defines basic operations
    /// </summary>
    public class Polynom
    {
        List<Monomial> monomials;

        public Polynom(List<Monomial> monomials)
        {
            this.monomials = monomials;
        }
        public Polynom()
        {
            monomials = new List<Monomial>();
        }

        /// <summary>
        /// Operation of adding two polynoms
        /// </summary>
        /// <param name="p1">1st polynom</param>
        /// <param name="p2">2nd polynom</param>
        /// <returns>sum of polynoms</returns>
        public static Polynom operator +(Polynom p1, Polynom p2)
        {
            List<Monomial> m1Copy = new List<Monomial>(p1.monomials);
            List<Monomial> m2Copy = new List<Monomial>(p2.monomials);

            for (int i = 0; i < m1Copy.Count; i++)
            {
                Monomial m1 = m1Copy[i];

                for (int j = 0; j < m2Copy.Count; j++)
                {
                    Monomial m2 = m2Copy[j];

                    if (m1.xPower == m2.xPower)
                    {
                        m1.coefficient += m2.coefficient;
                        m2Copy.Remove(m2);
                        j--;
                    }
                }

                if (m1.coefficient == 0)
                {
                    m1Copy.Remove(m1);
                    i--;
                }
            }

            m1Copy.AddRange(m2Copy);

            return new Polynom(m1Copy);
        }

        /// <summary>
        /// Subtraction operation of 2 polynomials
        /// </summary>
        /// <param name="p1">1st polynom</param>
        /// <param name="p2">2nd polynom</param>
        /// <returns>polynoms difference</returns>
        public static Polynom operator -(Polynom p1, Polynom p2)
        {
            List<Monomial> m1Copy = new List<Monomial>(p1.monomials);
            List<Monomial> m2Copy = new List<Monomial>(p2.monomials);

            for (int i = 0; i < m1Copy.Count; i++)
            {
                Monomial m1 = m1Copy[i];

                for (int j = 0; j < m2Copy.Count; j++)
                {
                    Monomial m2 = m2Copy[j];

                    if (m1.xPower == m2.xPower)
                    {
                        m1.coefficient -= m2.coefficient;
                        m2Copy.Remove(m2);
                        j--;
                    }
                }

                if (m1.coefficient == 0)
                {
                    m1Copy.Remove(m1);
                    i--;
                }
            }

            m2Copy.ForEach(x => x.coefficient = -x.coefficient);
            m1Copy.AddRange(m2Copy);

            return new Polynom(m1Copy);
        }

        /// <summary>
        /// Operation of multiplying a polynomial by a polynomial
        /// </summary>
        /// <param name="p1">1st polynom</param>
        /// <param name="p2">2nd polynom</param>
        /// <returns>multiplication of polynoms</returns>
        public static Polynom operator *(Polynom p1, Polynom p2)
        {
            List<Monomial> newPolyMonomials = new List<Monomial>();

            for (int i = 0; i < p1.monomials.Count; i++)
            {
                for (int j = 0; j < p2.monomials.Count; j++)
                {
                    Monomial m1 = p1.monomials[i];
                    Monomial m2 = p2.monomials[j];
                    newPolyMonomials.Add(new Monomial(m1.xPower + m2.xPower, m1.coefficient * m2.coefficient));
                }
            }

            if(newPolyMonomials.Count != 0)
            {
                //to reduce monomials, divide newPolyMonomials into 2 lists and summarize
                List<Monomial> firstHalf = newPolyMonomials.Take(newPolyMonomials.Count / 2).ToList();
                List<Monomial> secondHalf = newPolyMonomials.Skip(firstHalf.Count).ToList();
                return new Polynom(firstHalf) + new Polynom(secondHalf);
            }
            else
            {
                return new Polynom(newPolyMonomials);
            }
        }

        /// <summary>
        /// Operation of dividing a polynomial into a polynomial
        /// </summary>
        /// <param name="p1">1st polynom - dividend</param>
        /// <param name="p2">2nd polynom - divisor</param>
        /// <returns>quotient</returns>
        public static (Polynom, Polynom) operator /(Polynom p1, Polynom p2)
        {
            Polynom dividend = new Polynom(p1.monomials);
            Polynom divisor = new Polynom(p2.monomials);
            Polynom result = new Polynom();

            double maxPowerDividentX = dividend.monomials.Select(x => x.xPower).Max();
            double maxPowerDivisorX = divisor.monomials.Select(x => x.xPower).Max();
            Monomial divisorMonomialWithGreaterPowerX = divisor.monomials.Where(x => x.xPower == maxPowerDivisorX).First();

            if(maxPowerDivisorX > maxPowerDividentX)
            {
                throw new Exception("The degree of the divisor cannot be greater than the degree of dividend");
            }

            do
            {
                maxPowerDividentX = dividend.monomials.Select(x => x.xPower).Max();
                Monomial dividendMonomialWithGreaterPowerX = dividend.monomials.Where(x => x.xPower == maxPowerDividentX).First();

                double monomialPowerOfX = maxPowerDividentX - maxPowerDivisorX;
                double monomialCoeff = dividendMonomialWithGreaterPowerX.coefficient / divisorMonomialWithGreaterPowerX.coefficient;
                Polynom monomialPoly = new Polynom(new List<Monomial> { new Monomial(monomialPowerOfX, monomialCoeff) });
                result += monomialPoly;
                dividend -= monomialPoly * divisor;
            }
            while (maxPowerDividentX > maxPowerDivisorX);

            return (result, dividend);
        }

        /// <summary>
        /// Operation of multiplying a polynom by a number
        /// </summary>
        /// <param name="p1">polynom</param>
        /// <param name="number">number</param>
        /// <returns>multiplied polynom</returns>
        public static Polynom operator *(Polynom p1, double number)
        {
            List<Monomial> newPolyMonomials = new List<Monomial>();

            if(number != 0)
            {
                for (int i = 0; i < p1.monomials.Count; i++)
                {
                    newPolyMonomials.Add(
                        new Monomial(
                            p1.monomials[i].xPower, 
                            p1.monomials[i].coefficient * number));
                }
            }

            return new Polynom(newPolyMonomials);
        }

        /// <summary>
        /// Operation of dividing a polynom by a number
        /// </summary>
        /// <param name="p1">polynom</param>
        /// <param name="number">number</param>
        /// <returns>divided polynom</returns>
        public static Polynom operator /(Polynom p1, double number)
        {
            List<Monomial> newPolyMonomials = new List<Monomial>();

            if (number != 0)
            {
                for (int i = 0; i < p1.monomials.Count; i++)
                {
                    newPolyMonomials.Add(
                        new Monomial(
                            p1.monomials[i].xPower,
                            p1.monomials[i].coefficient / number));
                }
            }
            else
            {
                throw new DivideByZeroException("Can't divide polynom by zero");
            }

            return new Polynom(newPolyMonomials);
        }

        /// <summary>
        /// Get string representation of the Polynom class instance
        /// </summary>
        /// <returns>string representation of a class instance</returns>
        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder(monomials.Count * 5);

            foreach(Monomial monomial in monomials)
            {
                stringBuilder.Append(monomial.ToString() + "+");
            }

            if (stringBuilder.Length == 0)
            {
                return string.Empty;
            }
            else
            {
                return stringBuilder.ToString(0, stringBuilder.Length - 1).Replace("+-", "-");
            }
        }

        /// <summary>
        /// Determines whether two instances of an object are equal
        /// </summary>
        /// <param name="obj">2nd instance for comparsion</param>
        /// <returns>is equals</returns>
        public override bool Equals(object obj)
        {
            if (obj != null && obj is Polynom)
            {
                Polynom polynom = (Polynom)obj;

                if (new List<Monomial>(polynom.monomials).Except(monomials).Count() == 0 &&
                   new List<Monomial>(monomials).Except(polynom.monomials).Count() == 0)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Serves as a default hash function
        /// </summary>
        /// <returns>instance hash code</returns>
        public override int GetHashCode()
        {
            return monomials.GetHashCode();
        }
    }
}
