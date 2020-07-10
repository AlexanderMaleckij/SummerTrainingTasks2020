using System;

namespace Task2Figures
{
    public class Circle : Figure
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

        /// <summary>
        /// Constructor that creates an instance of 
        /// the Circle class on its specified radius
        /// </summary>
        /// <param name="radius">radius</param>
        public Circle(double radius)
        {
            Radius = radius;
        }

        /// <summary>
        /// Calculates the area of a circle
        /// </summary>
        /// <returns>area</returns>
        public override double Area() => Math.PI * Math.Pow(radius, 2);

        /// <summary>
        /// Calculates the perimeter of a circle
        /// </summary>
        /// <returns>perimeter</returns>
        public override double Perimeter() => 2 * Math.PI * Radius;

        /// <summary>
        /// String representation of a class instance
        /// </summary>
        /// <returns>String representation of a class instance</returns>
        public override string ToString() => $"Circle {radius}";

        /// <summary>
        /// Serves as a default hash function
        /// </summary>
        /// <returns>instance hash code</returns>
        public override int GetHashCode() => radius.GetHashCode();

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
                Circle circle = (Circle)obj;
                return circle.radius == radius;
            }
        }
    }
}
