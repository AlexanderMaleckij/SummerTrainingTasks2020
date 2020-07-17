using System;
using System.Collections.Generic;
using System.Drawing;

namespace Figures
{
    public class Rectangle : Figure
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
                else
                {
                    throw new ArgumentException(negativeSideLengthMsg);
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
                else
                {
                    throw new ArgumentException(negativeSideLengthMsg);
                }
            }
        }

        /// <summary>
        /// Constructor that creates an instance of 
        /// the Rectangle class on its specified sides
        /// </summary>
        /// <param name="a">1st side length</param>
        /// <param name="b">2nd side length</param>
        public Rectangle(double a, double b)
        {
            Side1Length = a;
            Side2Length = b;
        }

        /// <summary>
        /// Constructor that creates an instance of 
        /// the Rectangle class on its specified tops
        /// </summary>
        /// <param name="p1">1st top coords</param>
        /// <param name="p2">2nd top coords</param>
        /// <param name="p3">3rd top coords</param>
        /// <param name="p4">4th top coords</param>
        public Rectangle(PointF p1, PointF p2, PointF p3, PointF p4)
        {
            if(!IsRectangle(p1, p2, p3, p4, out double side1Length, out double side2Length))
            {
                throw new Exception("Points don't form a rectangle!");
            }
            else
            {
                Side1Length = side1Length;
                Side2Length = side2Length;
            }
        }

        /// <summary>
        /// Constructor for cutting a rectangle with 
        /// given sides from a given figure
        /// </summary>
        /// <param name="a">1st side length</param>
        /// <param name="b">2nd side length</param>
        /// <param name="figure">figure to cut</param>
        public Rectangle(double a, double b, Figure figure) : this(a, b)
        {
            if (Area() > figure.Area())
            {
                throw new CuttingException("Can't cut a rectangle with a given sides from this figure");
            }
        }

        /// <summary>
        /// Constructor for cutting a rectangle by 
        /// given vertices from a given figure
        /// </summary>
        /// <param name="p1">1st top coords</param>
        /// <param name="p2">2nd top coords</param>
        /// <param name="p3">3rd top coords</param>
        /// <param name="p4">4th top coords</param>
        /// <param name="figure">figure to cut</param>
        public Rectangle(PointF p1, PointF p2, PointF p3, PointF p4, Figure figure) : this(p1, p2, p3, p4)
        {
            if (Area() > figure.Area())
            {
                throw new CuttingException("Can't cut a rectangle with a given vertex coordinates from this figure");
            }
        }

        /// <summary>
        /// Determines if the shape with 
        /// the indicated vertices is a rectangle
        /// </summary>
        /// <param name="a">1st top coords</param>
        /// <param name="b">2nd top coords</param>
        /// <param name="c">3rd top coords</param>
        /// <param name="d">4th top coords</param>
        /// <param name="side1Length">calculated 1st side length</param>
        /// <param name="side2Length">calculated 2nd side length</param>
        /// <returns></returns>
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
                        if (permutation[4] == Math.Sqrt(Math.Pow(permutation[0], 2) + Math.Pow(permutation[1], 2)))
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

        /// <summary>
        /// Calculates the area of a rectangle
        /// </summary>
        /// <returns>area</returns>
        public override double Area() 
        { 
            return Side1Length* Side2Length; 
        }

        /// <summary>
        /// Calculates the perimeter of a rectangle
        /// </summary>
        /// <returns>perimeter</returns>
        public override double Perimeter() 
        { 
            return 2 * (Side1Length + Side2Length); 
        }

        /// <summary>
        /// String representation of a class instance
        /// </summary>
        /// <returns>string representation of a class instance</returns>
        public override string ToString() 
        { 
            return $"Rectangle {Side1Length} {Side2Length}"; 
        }

        /// <summary>
        /// Serves as a default hash function
        /// </summary>
        /// <returns>instance hash code</returns>
        public override int GetHashCode()
        {
            return (Side1Length.GetHashCode() << 2) ^ Side2Length.GetHashCode(); 
        }

        /// <summary>
        /// Determines whether two instances of an object are equal
        /// </summary>
        /// <param name="obj">2nd instance for comparsion<</param>
        /// <returns>is equals</returns>
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

        public override object Clone()
        {
            return new Rectangle(Side1Length, Side2Length);
        }
    }
}
