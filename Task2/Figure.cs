using System;
using System.Drawing;

namespace Task2Figures
{
    public abstract class Figure
    {
        public abstract double Area();
        public abstract double Perimeter();

        /// <summary>
        /// Calculate distance between points
        /// </summary>
        /// <param name="a">1st point</param>
        /// <param name="b">2nd point</param>
        /// <returns>distance between points</returns>
        protected static double Distance(PointF a, PointF b) => Math.Abs(Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2));
    }
}
