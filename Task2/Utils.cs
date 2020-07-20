using System;
using System.Collections.Generic;
using System.Drawing;

namespace Task2Figures
{
    class Utils
    {
        /// <summary>
        /// looks in the array for all shapes equal to a given
        /// </summary>
        /// <param name="desiredShape">searching shape</param>
        /// <param name="shapes">figures where to look</param>
        /// <returns>shapes equal to a desiredShape</returns>
        internal static Figure[] FindEqualsFigures(Figure desiredShape, Figure[] shapes)
        {
            List<Figure> desiredFigures = new List<Figure>();

            for (int i = 0; i < shapes.Length; i++)
            {
                if (desiredShape.Equals(shapes[i]))
                {
                    desiredFigures.Add(shapes[i]);
                }
            }

            return desiredFigures.ToArray();
        }

        /// <summary>
        /// Calculate distance between points
        /// </summary>
        /// <param name="a">1st point</param>
        /// <param name="b">2nd point</param>
        /// <returns>distance between points</returns>
        internal static double Distance(PointF a, PointF b)
        {
            return Math.Sqrt(Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2));
        }
    }
}
