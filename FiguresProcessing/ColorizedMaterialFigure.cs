using ColorMaterial;
using Figures;

namespace FiguresProcessing
{
    public partial class Box
    {
        public class ColorizedMaterialFigure
        {
            public ColoratedMaterial ColoratedMaterial { get; private set; }
            public Figure Figure { get; private set; }

            public ColorizedMaterialFigure(Figure figure, ColoratedMaterial coloratedMaterial)
            {
                Figure = figure;
                ColoratedMaterial = coloratedMaterial;
            }
        }

    }
}
