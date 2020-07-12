using System;
using System.Collections.Generic;
using System.Linq;

namespace Task2
{
    public class Polynom
    {
        List<Monomial> monomials;

        public Polynom(string polynom)
        {
            monomials = MonomialsParser.Parse(polynom);
        }

        private Polynom(List<Monomial> monomials)
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
    }
}
