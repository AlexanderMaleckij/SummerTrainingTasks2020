using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Task2
{
    public class Polynom
    {
        List<Monomial> monomials;

        public Polynom(List<Monomial> monomials)
        {
            this.monomials = monomials;
        }

        public static Polynom operator +(Polynom p1, Polynom p2)
        {
            List<Monomial> m1Copy = new List<Monomial>(p1.monomials);
            List<Monomial> m2Copy = new List<Monomial>(p2.monomials);

            for (int i = 0; i < m1Copy.Count; i++)
            {
                for(int j = 0; j < m2Copy.Count; j++)
                {
                    Monomial m1 = m1Copy[i];
                    Monomial m2 = m2Copy[j];

                    if (m1.xPower == m2.xPower)
                    {
                        m1.coefficient += m2.coefficient;
                        m2Copy.Remove(m2);
                        j--;
                    }

                    if(m1.coefficient == 0)
                    {
                        m1Copy.Remove(m1);
                        i--;
                    }
                }
            }

            m1Copy.AddRange(m2Copy);

            return new Polynom(m1Copy);
        }

        public static Polynom operator -(Polynom p1, Polynom p2)
        {
            List<Monomial> m1Copy = new List<Monomial>(p1.monomials);
            List<Monomial> m2Copy = new List<Monomial>(p2.monomials);

            for (int i = 0; i < m1Copy.Count; i++)
            {
                for (int j = 0; j < m2Copy.Count; j++)
                {
                    Monomial m1 = m1Copy[i];
                    Monomial m2 = m2Copy[j];

                    if (m1.xPower == m2.xPower)
                    {
                        m1.coefficient -= m2.coefficient;
                        m2Copy.Remove(m2);
                        j--;
                    }

                    if (m1.coefficient == 0)
                    {
                        m1Copy.Remove(m1);
                        i--;
                    }
                }
            }
            return new Polynom(m1Copy);
        }

        public static Polynom operator *(Polynom p1, Polynom p2)
        {
            List<Monomial> newPolyMonomials = new List<Monomial>();

            for (int i = 0; i < p1.monomials.Count; i++)
            {
                for (int j = 0; j < p2.monomials.Count; j++)
                {
                    Monomial m1 = p1.monomials[i];
                    Monomial m2 = p2.monomials[j];
                    newPolyMonomials.Add(new Monomial(m1.xPower * m2.xPower, m1.coefficient * m2.coefficient));
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

        public static (Polynom, Polynom) operator /(Polynom p1, Polynom p2)
        {
            double[] GetCoeffs(Polynom p)
            {
                int maxPowerOfX = p.monomials.Select(x => x.xPower).Max();
                double[] coeffs = new double[maxPowerOfX];

                foreach (Monomial monomial in p.monomials)
                {
                    coeffs[monomial.xPower] += monomial.coefficient;
                }
                coeffs.Reverse();

                return coeffs;
            }

            Polynom GetPolynom(double[] coeffs)
            {
                List<Monomial> monomials = new List<Monomial>();

                for(int i = 0; i < coeffs.Length; i++)
                {
                    if(coeffs[i] != 0)
                    {
                        monomials.Add(new Monomial(coeffs.Length - i, coeffs[i]));
                    }
                }

                return new Polynom(monomials);
            }

            double[] dividend = GetCoeffs(p1);
            double[] divisor = GetCoeffs(p2);
            double[] remainder = (double[])dividend.Clone();
            double[] result = new double[remainder.Length - divisor.Length + 1];

            if (dividend.Last() == 0)
            {
                throw new ArithmeticException("Старший член многочлена делимого не может быть 0");
            }
            if (divisor.Last() == 0)
            {
                throw new ArithmeticException("Старший член многочлена делителя не может быть 0");
            }

            for (int i = 0; i < result.Length; i++)
            {
                double coeff = remainder[remainder.Length - i - 1] / divisor.Last();
                result[result.Length - i - 1] = coeff;
                for (int j = 0; j < divisor.Length; j++)
                {
                    remainder[remainder.Length - i - j - 1] -= coeff * divisor[divisor.Length - j - 1];
                }
            }

            return (GetPolynom(result), GetPolynom(remainder));
        }

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

        public override int GetHashCode()
        {
            return monomials.GetHashCode();
        }
    }
}
