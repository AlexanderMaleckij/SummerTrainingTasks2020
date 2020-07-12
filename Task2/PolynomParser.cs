﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Task2
{
    static class MonomialsParser
    {
        public static List<Monomial> Parse(string polynom)
        {
            ValidatePolynom(polynom);



           

        }

        private static void ValidatePolynom(string polynom)
        {
            RemoveExtremeBrackets(polynom);
            CheckBrackets(polynom);
            CheckPolynomFormat(polynom);
            CheckForMultiplePolynomials(polynom);
        }

        private static void RemoveExtremeBrackets(string polynom)
        {
            polynom = polynom.Trim();

            if (polynom.StartsWith("(") && polynom.EndsWith(")"))
            {
                polynom = polynom.Substring(1, polynom.Length - 2);
            }
        }

        private static void CheckBrackets(string polynom)
        {
            if (polynom.Where(x => x == '(').Count() != polynom.Where(x => x == ')').Count())
            {
                throw new ArgumentException("The number of opening and closing brackets doesn't match");
            }
        }

        private static void CheckPolynomFormat(string polynom)
        {
            Regex regex = new Regex(@"[^x\d^+*\-\.\(\)]|[\(\)x]{2,}|^[^\dx]$|[+\-*\.,^]{2,}|\^x|\d\*[^x]|x\d|\dx|[+\^]$|\([\D]\)");
            if (regex.IsMatch(polynom))
            {
                string errors = string.Empty;
                foreach (Match match in regex.Matches(polynom))
                {
                    errors += '\n' + match.Value;
                }
                throw new ArgumentException($"Polynom format error: {errors}");
            }
        }

        private static void CheckForMultiplePolynomials(string polynom)
        {
            foreach (Match match in new Regex(@"\((.+)\)").Matches(polynom))
            {
                if (match.Value.Contains('('))
                {
                    throw new ArgumentException("Multiple polynomials found in a given line");
                }
            }
        }
    }
}
