using System;
using System.Drawing;

namespace Task2
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

        public bool IsSquare(PointF p1, PointF p2, PointF p3, PointF p4, out double sideLength)
        {

        }

        public override double Area() => Math.Pow(SideLength, 2);

        public override double Perimeter() => 4 * SideLength;
    }
}
