using System;

namespace Task2
{
    class Circle : Figure
    {
        double radius;

        public double Radius
        {
            get => radius;
            set
            {
                if(value > 0)
                {
                    radius = value;
                }
            }
        }

        public Circle(double radius)
        {
            Radius = radius;
        }

        public override double Area() => Math.PI * Math.Pow(radius, 2);

        public override double Perimeter() => 2 * Math.PI * Radius;
    }
}
