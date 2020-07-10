using System;

namespace Task2Figures
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

        public override string ToString() => $"Circle radius={radius}; Area={Area()}; Perimeter={Perimeter()}";

        public override int GetHashCode() => radius.GetHashCode();

        public override bool Equals(object obj)
        {
            if (obj is null || !GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Circle circle = (Circle)obj;
                return circle.radius == radius;
            }
        }
    }
}
