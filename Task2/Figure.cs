using System;
using System.Drawing;

namespace Task2
{
    public abstract class Figure
    {
        public abstract double Area();
        public abstract double Perimeter();
        protected static double Distance(PointF a, PointF b) => Math.Abs(Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2));
    }
}
