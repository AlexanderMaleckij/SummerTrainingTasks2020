using ColorMaterial;
using Figures;
using System;
using System.Collections.Generic;

namespace FiguresProcessing
{
    public partial class Box
    {
        const int boxCapacity = 20;
        public int AmountOfFigures { get; private set; }

        ColorizedMaterialFigure[] colorizedMaterialFigures = new ColorizedMaterialFigure[boxCapacity];

        private ColorizedMaterialFigure this[int index]
        {
            get
            {
                if (index < 0 || index >= boxCapacity)
                {
                    throw new IndexOutOfRangeException("The specified figure number doesn't exist");
                }
                else
                {
                    return colorizedMaterialFigures[index];
                }
            }
            set
            {
                if (index < 0 || index >= boxCapacity)
                {
                    throw new IndexOutOfRangeException($"Number must be between 0 and {boxCapacity - 1}");
                }
                else
                {
                    colorizedMaterialFigures[index] = value;
                }
            }
        }

        public void AddFigure(ColorizedMaterialFigure figure)
        {
            if(AmountOfFigures < 20)
            {
                this[AmountOfFigures] = figure;
                AmountOfFigures++;
            }
            else
            {
                throw new OverflowException("The box is already full");
            }
        }
        
        public ColorizedMaterialFigure GetCopy(int number)
        {
            return (ColorizedMaterialFigure)this[number].Clone();
        }

        public ColorizedMaterialFigure ExtractFigure(int number)
        {
            if(AmountOfFigures == 0)
            {
                return null;
            }
            else
            {
                ColorizedMaterialFigure requiredFigure = this[number];

                AmountOfFigures -= 1;

                for (int i = number; i < boxCapacity; i++)
                {
                    colorizedMaterialFigures[i] = colorizedMaterialFigures[i + 1];
                }

                return requiredFigure;
            }
        }

        public void Replace(int number, ColorizedMaterialFigure replacementFigure)
        {
            this[number] = replacementFigure;
        }

        public ColorizedMaterialFigure Find(ColorizedMaterialFigure desiredFigure)
        {
            ColorizedMaterialFigure foundFigure = null;

            foreach(ColorizedMaterialFigure figure in colorizedMaterialFigures)
            {
                if (figure.Equals(desiredFigure))
                {
                    foundFigure = figure;
                }
            }

            return foundFigure;
        }

        public double CalcTotalArea()
        {
            double area = 0;

            foreach(ColorizedMaterialFigure figure in colorizedMaterialFigures)
            {
                area += figure.Figure.Area();
            }

            return area;
        }

        public double CalcTotalPerimeter()
        {
            double perimeter = 0;

            foreach (ColorizedMaterialFigure figure in colorizedMaterialFigures)
            {
                perimeter += figure.Figure.Perimeter();
            }

            return perimeter;
        }

        public List<ColorizedMaterialFigure> GetAllCircles()
        {
            List<ColorizedMaterialFigure> circles = new List<ColorizedMaterialFigure>();

            foreach(ColorizedMaterialFigure figure in colorizedMaterialFigures)
            {
                if(figure.Figure is Circle)
                {
                    circles.Add(figure);
                }
            }

            return circles;
        }

        public List<ColorizedMaterialFigure> GetAllFilmFigures()
        {
            List<ColorizedMaterialFigure> filmFigures = new List<ColorizedMaterialFigure>();

            foreach (ColorizedMaterialFigure figure in colorizedMaterialFigures)
            {
                if (figure.ColoratedMaterial is Film)
                {
                    filmFigures.Add(figure);
                }
            }

            return filmFigures;
        }
    }
}
