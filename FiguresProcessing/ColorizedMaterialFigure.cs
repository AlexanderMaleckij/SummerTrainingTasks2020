using ColorMaterial;
using Figures;
using System;

namespace FiguresProcessing
{
    public partial class Box
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
        }
    }
}
