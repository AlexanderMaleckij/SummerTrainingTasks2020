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

        }
        public static Polynom operator *(Polynom p1, double number)
        {

        }
        public static Polynom operator /(Polynom p1, double number)
        {

        }

        private static List<Monomial> GetMonomials(Polynom p1, Polynom p2)
        {
            List<Monomial> monomials = new List<Monomial>(p1.monomials);
            monomials.AddRange(p2.monomials);
            return monomials;
        }
    }
}
