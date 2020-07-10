using System.Drawing;

namespace Task2
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

        }

        public Rectangle(PointF p1, PointF p2, PointF p3, PointF p4)
        {
            if(IsRectangle(p1, p2, p3, p4, out double side1Length, out double side2Length))
            {
                Side1Length = side1Length;
                Side2Length = side2Length;
            }
        }

        private bool IsRectangle(PointF p1, PointF p2, PointF p3, PointF p4, out double a, out double b)
        {

        }

        public override double Area() => Side1Length * Side2Length;

        public override double Perimeter() => 2 * (Side1Length + Side2Length);
    }
}
