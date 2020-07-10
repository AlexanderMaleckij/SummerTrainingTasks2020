using System;
using System.Collections.Generic;
using System.Drawing;

namespace Task2Figures
{
    class Rectangle : Figure
    {
        private double side1Length;
        private double side2Length;

        public double Side1Length
        {
            get => side1Length;
            set
            {
                if(value > 0)
                {
                    side1Length = value;
                }
            }
        }

        public double Side2Length
        {
            get => side2Length;
            set
            {
                if (value > 0)
                {
                    side2Length = value;
                }
            }
        }

        public Rectangle(double a, double b)
        {
            Side1Length = side1Length;
            Side2Length = side2Length;
        }

        public Rectangle(PointF p1, PointF p2, PointF p3, PointF p4)
        {
            if(IsRectangle(p1, p2, p3, p4, out double side1Length, out double side2Length))
            {
                Side1Length = side1Length;
                Side2Length = side2Length;
            }
        }

        private static bool IsRectangle(PointF a, PointF b, PointF c, PointF d, out double side1Length, out double side2Length)
        {
            double d0 = Distance(a, b);
            double d1 = Distance(a, c);
            double d2 = Distance(a, d);
            double d3 = Distance(b, c);
            double d4 = Distance(b, d);
            double d5 = Distance(c, d);
            //get all possible combinations of the sides lengthes
            List<List<double>> permutations = Permutations.GetPermutations(new List<double> { d0, d1, d2, d3, d4, d5 });
            foreach (List<double> permutation in permutations)
            {
                //if sides of square are equal
                if ((permutation[0] == permutation[2]) == (permutation[1] == permutation[3]) == true)
                {
                    //if diagonals are equal
                    if (permutation[4] == permutation[5])
                    {
                        //if diagonals are linked to the sides
                        if (Math.Pow(permutation[4], 2) == Math.Pow(permutation[0], 2) + Math.Pow(permutation[1], 2))
                        {
                            side1Length = permutation[0];
                            side2Length = permutation[1];
                            return true;
                        }
                    }
                }
            }
            side1Length = side2Length = default;
            return false;
        }

        public override double Area() => Side1Length * Side2Length;

        public override double Perimeter() => 2 * (Side1Length + Side2Length);

        public override string ToString() => $"Rectangle a={Side1Length}; b={Side2Length}; Area={Area()}; Perimeter={Perimeter()}";

        public override int GetHashCode() => (Side1Length.GetHashCode() << 2) ^ Side2Length.GetHashCode();

        public override bool Equals(object obj)
        {
            if (obj is null || !GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Rectangle rectangle = (Rectangle)obj;
                return (rectangle.side1Length == side1Length) && (rectangle.side2Length == side2Length);
            }
        }
    }
}
