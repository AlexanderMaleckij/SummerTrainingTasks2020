using System;

namespace Figures
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
                else
                {
                    throw new ArgumentException(negativeSideLengthMsg);
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
        /// Constructor for cutting a circle with 
        /// given radius from a given figure
        /// </summary>
        /// <param name="radius">radius</param>
        /// <param name="figure">figure to cut</param>
        public Circle(double radius, Figure figure) : this(radius)
        {
            if (Area() > figure.Area())
            {
                throw new CuttingException("Can't cut a circle with a given radius from this shape");
            }
        }

        /// <summary>
        /// Calculates the area of a circle
        /// </summary>
        /// <returns>area</returns>
        public override double Area() 
        {
            return Math.PI* Math.Pow(radius, 2); 
        }

        /// <summary>
        /// Calculates the perimeter of a circle
        /// </summary>
        /// <returns>perimeter</returns>
        public override double Perimeter() 
        { 
            return 2 * Math.PI * Radius; 
        }

        /// <summary>
        /// String representation of a class instance
        /// </summary>
        /// <returns>String representation of a class instance</returns>
        public override string ToString() 
        { 
            return $"Circle {radius}"; 
        }

        /// <summary>
        /// Serves as a default hash function
        /// </summary>
        /// <returns>instance hash code</returns>
        public override int GetHashCode() 
        { 
            return radius.GetHashCode(); 
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
                Circle circle = (Circle)obj;
                return circle.radius == radius;
            }
        }
    }
}
