using System;
using System.Collections.Generic;
using System.Drawing;
using Task2Figures;

namespace Task2Figures
{
    class Square : Figure
    {
        double sideLength;

        public double SideLength
        {
            get => sideLength;
            set
            {
                if (value > 0)
                {
                    sideLength = value;
                }
            }
        }

        public Square(double sideLength)
        {
            SideLength = sideLength;
        }

        public Square(PointF p1, PointF p2, PointF p3, PointF p4)
        {
            if(IsSquare(p1, p2, p3, p4, out double sideLength))
            {
                SideLength = sideLength;
            }
        }

        public static bool IsSquare(PointF a, PointF b, PointF c, PointF d, out double sideLength)
        {
            double d0 = Distance(a, b);
            double d1 = Distance(a, c);
            double d2 = Distance(a, d);
            double d3 = Distance(b, c);
            double d4 = Distance(b, d);
            double d5 = Distance(c, d);
            //get all possible combinations of the sides lengthes
            List<List<double>> permutations = Permutations.GetPermutations(new List<double> { d0, d1, d2, d3, d4, d5});
            foreach(List<double> permutation in permutations)
            {
                //if sides of square are equal
                if((permutation[0] == permutation[1]) == (permutation[2] == permutation[3]) == true)
                {
                    //if diagonals are equal
                    if(permutation[4] == permutation[5])
                    {
                        //if diagonals are linked to the sides
                        if(permutation[4] == permutation[0] * Math.Sqrt(2))
                        {
                            sideLength = permutation[0];
                            return true;
                        }
                    }
                }
            }
            sideLength = default;
            return false;
        }

        public override double Area() => Math.Pow(SideLength, 2);

        public override double Perimeter() => 4 * SideLength;
    }
}
