using System;
using System.Drawing;

namespace Task2Figures
{
    public class Triangle : Figure
    {
        double side1Length;
        double side2Length;
        double side3Length;

        public double Side1Length
        {
            get => side1Length;
            set
            {
                if (value > 0)
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

        public double Side3Length
        {
            get => side3Length;
            set
            {
                if (value > 0)
                {
                    side3Length = value;
                }
                else
                {
                    throw new ArgumentException(negativeSideLengthMsg);
                } 
            }
        }


        /// <summary>
        /// Constructor that creates an instance of 
        /// the Triangle class on its specified sides
        /// </summary>
        /// <param name="a">1st side length</param>
        /// <param name="b">2nd side length</param>
        /// <param name="c">3rd side length</param>
        public Triangle(double a, double b, double c)
        {
            Side1Length = a;
            Side2Length = b;
            Side3Length = c;
        }

        /// <summary>
        /// Constructor that creates an instance of 
        /// the Triangle class on its specified tops
        /// </summary>
        /// <param name="p1">1st top coords</param>
        /// <param name="p2">2nd top coords</param>
        /// <param name="p3">3rd top coords</param>
        public Triangle(PointF p1, PointF p2, PointF p3)
        {
            if(!IsTriangle(p1, p2, p3))
            {
                throw new Exception("Triangle points can't lie on one line!");
            }
            else
            {
                Side1Length = Utils.Distance(p1, p2);
                Side2Length = Utils.Distance(p2, p3);
                Side3Length = Utils.Distance(p3, p1);
            }
        }

        /// <summary>
        /// Determines if the shape with 
        /// the indicated vertices is a triangle
        /// </summary>
        /// <param name="p1">1st top coords</param>
        /// <param name="p2">2nd top coords</param>
        /// <param name="p3">3rd top coords</param>
        /// <returns>is triangle</returns>
        private static bool IsTriangle(PointF p1, PointF p2, PointF p3)
        {
            if ((p2.X - p1.X)*(p3.Y - p1.Y) - (p2.Y - p1.Y)*(p3.X - p1.X) == 0) //if three points lay on one line
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Calculates the area of a triangle
        /// </summary>
        /// <returns>area</returns>
        public override double Area()
        {
            double halfPerim = Perimeter() / 2;
            return Math.Sqrt(halfPerim * (halfPerim - Side1Length) * (halfPerim - Side2Length) * (halfPerim - Side3Length));
        }

        /// <summary>
        /// Calculates the perimeter of a triangle
        /// </summary>
        /// <returns>perimeter</returns>
        public override double Perimeter() => Side1Length + Side2Length + Side3Length;

        /// <summary>
        /// String representation of a class instance
        /// </summary>
        /// <returns>string representation of a class instance</returns>
        public override string ToString() => $"Triangle {Side1Length} {Side2Length} {Side3Length}";

        /// <summary>
        /// Serves as a default hash function
        /// </summary>
        /// <returns>instance hash code</returns>
        public override int GetHashCode() => (Side1Length.GetHashCode() << 3) ^ (Side2Length.GetHashCode() << 2) ^ Side3Length.GetHashCode();

        /// <summary>
        /// Determines whether two instances of an object are equal
        /// </summary>
        /// <param name="obj">2nd instance for comparsion</param>
        /// <returns>is equals</returns>
        public override bool Equals(object obj)
        {
            if(obj is null || !GetType().Equals(obj.GetType()))
            {
                return false;
            }    
            else
            {
                Triangle triangle = (Triangle)obj;
                return (triangle.side1Length == side1Length) && (triangle.side2Length == side2Length) && (triangle.side3Length == side3Length);
            }
        }
    }
}
