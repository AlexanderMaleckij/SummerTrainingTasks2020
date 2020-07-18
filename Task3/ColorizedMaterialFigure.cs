using Figures;
using System;

namespace ColorMaterial
{
    public class ColorizedMaterialFigure : ICloneable
    {
        public ColoratedMaterial ColoratedMaterial { get; private set; }
        public Figure Figure { get; private set; }

        public ColorizedMaterialFigure(Figure figure, ColoratedMaterial coloratedMaterial)
        {
            Figure = figure;
            ColoratedMaterial = coloratedMaterial;
        }

        public object Clone()
        {
            return new ColorizedMaterialFigure((Figure)Figure.Clone(), (ColoratedMaterial)ColoratedMaterial.Clone());
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
                ColorizedMaterialFigure instance = (ColorizedMaterialFigure)obj;
                return instance.ColoratedMaterial.Equals(ColoratedMaterial) &&
                       instance.Figure.Equals(Figure);
            }
        }

        /// <summary>
        /// Serves as a default hash function
        /// </summary>
        /// <returns>instance hash code</returns>
        public override int GetHashCode()
        {
            return (ColoratedMaterial.GetHashCode() << 1) ^ Figure.GetHashCode();
        }

        /// <summary>
        /// String representation of a class instance
        /// </summary>
        /// <returns>String representation of a class instance</returns>
        public override string ToString()
        {
            return Figure.ToString() + " " + ColoratedMaterial.ToString();
        }
    }
}
