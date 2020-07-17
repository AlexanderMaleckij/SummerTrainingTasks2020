
using System;

namespace ColorMaterial
{
    /// <summary>
    /// Parent class for material classes that can be colored
    /// </summary>
    public abstract class ColoratedMaterial : ICloneable
    {
        /// <summary>
        /// Auto property for getting and setting 
        /// the color of the material
        /// </summary>
        public Color Color { get; protected set; }

        /// <summary>
        /// Method for colorize material
        /// </summary>
        /// <param name="color">material color</param>
        public abstract void Colorize(Color color);

        public abstract object Clone();
    }
}
