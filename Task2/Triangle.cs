using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Task2Figures
{
    class Triangle : Figure
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
                    side1Length = value;
            }
        }

        public double Side2Length
        {
            get => side2Length;
            set
            {
                if (value > 0)
                    side2Length = value;
            }
        }

        public double Side3Length
        {
            get => side3Length;
            set
            {
                if (value > 0)
                    side3Length = value;
            }
        }

        public Triangle(double a, double b, double c)
        {
            Side1Length = a;
            Side2Length = b;
            Side3Length = c;
        }

        public Triangle(PointF p1, PointF p2, PointF p3)
        {
            if(IsTriangle(p1, p2, p3))
            {
                Side1Length = Distance(p1, p2);
                Side2Length = Distance(p2, p3);
                Side3Length = Distance(p3, p1);
            }
        }

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

        public override double Area()
        {
            double halfPerim = Perimeter() / 2;
            return Math.Sqrt(halfPerim * (halfPerim - Side1Length) * (halfPerim - Side2Length) * (halfPerim - Side3Length));
        }

        public override double Perimeter() => Side1Length + Side2Length + Side3Length;

        public override string ToString() => $"Triangle a={Side1Length}; b={Side2Length}; c={Side3Length}; Area={Area()}; Perimeter={Perimeter()}";

        public override int GetHashCode() => (Side1Length.GetHashCode() << 3) ^ (Side2Length.GetHashCode() << 2) ^ Side3Length.GetHashCode();

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
