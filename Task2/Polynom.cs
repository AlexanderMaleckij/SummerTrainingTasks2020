using System;
using System.Collections.Generic;

namespace Task2
{
    public class Polynom
    {
        List<Monomial> monomials;

        public Polynom(string polynom)
        {
            monomials = MonomialsParser.Parse(polynom);
        }
    }
}
