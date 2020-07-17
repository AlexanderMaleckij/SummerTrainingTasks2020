using System;
using System.Collections.Generic;
using System.Drawing;

namespace Figures
{
    public class Square : Figure
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
                else
                {
                    throw new ArgumentException(negativeSideLengthMsg);
                }
            }
        }

        /// <summary>
        /// Constructor that creates an instance of 
        /// the Square class on its specified side
        /// </summary>
        /// <param name="sideLength">side length</param>
        public Square(double sideLength)
        {
            SideLength = sideLength;
        }

        /// <summary>
        /// Constructor that creates an instance of 
        /// the Square class on its specified tops
        /// </summary>
        /// <param name="p1">1st top coords</param>
        /// <param name="p2">2nd top coords</param>
        /// <param name="p3">3rd top coords</param>
        /// <param name="p4">4th top coords</param>
        public Square(PointF p1, PointF p2, PointF p3, PointF p4)
        {
            if(!IsSquare(p1, p2, p3, p4, out double sideLength))
            {
                throw new Exception("Points don't form a square!");
            }
            else
            {
                SideLength = sideLength;
            }
        }

        /// <summary>
        /// Constructor for cutting a square with 
        /// given sides from a given figure
        /// </summary>
        /// <param name="sideLength">side length</param>
        /// <param name="figure">figure to cut</param>
        public Square(double sideLength, Figure figure) : this(sideLength)
        {
            if (Area() > figure.Area())
            {
                throw new CuttingException("Can't cut a square with a given side length from this figure");
            }
        }

        /// <summary>
        /// Constructor for cutting a square by 
        /// given vertices from a given figure
        /// </summary>
        /// <param name="p1">1st top coords</param>
        /// <param name="p2">2nd top coords</param>
        /// <param name="p3">3rd top coords</param>
        /// <param name="p4">4th top coords</param>
        /// <param name="figure">figure to cut</param>
        public Square(PointF p1, PointF p2, PointF p3, PointF p4, Figure figure) : this(p1, p2, p3, p4)
        {
            if (Area() > figure.Area())
            {
                throw new CuttingException("Can't cut a square with a given vertex coordinates from this figure");
            }
        }

        /// <summary>
        /// Determines if the shape with 
        /// the indicated vertices is a square
        /// </summary>
        /// <param name="a">1st top coords</param>
        /// <param name="b">2nd top coords</param>
        /// <param name="c">3rd top coords</param>
        /// <param name="d">4th top coords</param>
        /// <param name="sideLength"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Calculates the area of a square
        /// </summary>
        /// <returns>area</returns>
        public override double Area() 
        { 
            return Math.Pow(SideLength, 2); 
        }

        /// <summary>
        /// Calculates the perimeter of a square
        /// </summary>
        /// <returns>perimeter</returns>
        public override double Perimeter() 
        { 
            return 4 * SideLength; 
        }

        /// <summary>
        /// String representation of a class instance
        /// </summary>
        /// <returns>string representation of a class instance</returns>
        public override string ToString() 
        { 
            return $"Square {SideLength}"; 
        }

        /// <summary>
        /// Serves as a default hash function
        /// </summary>
        /// <returns>instance hash code</returns>
        public override int GetHashCode() 
        { 
            return SideLength.GetHashCode(); 
        }

        /// <summary>
        /// Determines whether two instances of an object are equal
        /// </summary>
        /// <param name="obj">2nd instance for comparsion</param>
        /// <returns>is equals</returns>
        public override bool Equals(object obj)
        {
            if (obj is null || !GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Square square = (Square)obj;
                return square.sideLength == sideLength;
            }
        }

        public override object Clone()
        {
            return new Square(SideLength);
        }
    }
}
