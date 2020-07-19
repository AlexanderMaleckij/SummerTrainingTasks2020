using ColorMaterial;
using DataRW;
using Figures;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FiguresProcessing
{
    public class Box
    {
        public const int boxCapacity = 20;
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
                if(Find(figure) != null)
                {
                    throw new ArgumentException("A figure with similar properties is already contained in the box");
                }
                else
                {
                    this[AmountOfFigures] = figure;
                    AmountOfFigures++;
                }
            }
            else
            {
                throw new OverflowException("The box is already full");
            }
        }
        
        public ColorizedMaterialFigure GetCopy(int number)
        {
            if(this[number] == null)
            {
                return null;
            }
            else
            {
                return (ColorizedMaterialFigure)this[number].Clone();
            }
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
            this[number] = (ColorizedMaterialFigure)replacementFigure.Clone();
        }

        public ColorizedMaterialFigure Find(ColorizedMaterialFigure desiredFigure)
        {
            for(int i = 0; i < AmountOfFigures; i++)
            {
                if (this[i].Equals(desiredFigure))
                {
                    return GetCopy(i);
                }
            }
            return null;
        }

        public double CalcTotalArea()
        {
            double area = 0;

            for(int i = 0; i < AmountOfFigures; i++)
            {
                area += this[i].Figure.Area();
            }

            return area;
        }

        public double CalcTotalPerimeter()
        {
            double perimeter = 0;

            foreach(int i in Enumerable.Range(0, AmountOfFigures))
            {
                perimeter += this[i].Figure.Perimeter();
            }

            return perimeter;
        }

        public ColorizedMaterialFigure[] GetAllCircles()
        {
            List<ColorizedMaterialFigure> circles = new List<ColorizedMaterialFigure>();

            for (int i = 0; i < AmountOfFigures; i++)
            {
                if (this[i].Figure is Circle)
                {
                    circles.Add(this[i]);
                }
            }

            return circles.ToArray();
        }

        public ColorizedMaterialFigure[] GetAllFilmFigures()
        {
            List<ColorizedMaterialFigure> filmFigures = new List<ColorizedMaterialFigure>();

            for(int i = 0; i < AmountOfFigures; i++)
            {
                if (this[i].ColoratedMaterial is Film)
                {
                    filmFigures.Add(this[i]);
                }
            }

            return filmFigures.ToArray();
        }

        public void SaveFigures(SaveMode saveMode, SaveMethod saveMethod, string fileName)
        {
            IDataRW dataRw;

            switch (saveMethod)
            {
                case SaveMethod.StreamWriter:
                    dataRw = new StreamDataRWXml(fileName);
                    break;
                case SaveMethod.XmlWriter:
                    dataRw = new XmlDataRWXml(fileName);
                    break;
                default:
                    throw new Exception($"Save method {saveMode} not supported");
            }

            dataRw.Write(SelectAppropriateFigures(saveMode));
        }

        public void LoadFigures(LoadMethod loadMethod, string fileName)
        {
            IDataRW dataRw;

            switch (loadMethod)
            {
                case LoadMethod.StreamReader:
                    dataRw = new StreamDataRWXml(fileName);
                    break;
                case LoadMethod.XmlReader:
                    dataRw = new XmlDataRWXml(fileName);
                    break;
                default:
                    throw new Exception($"Save method {loadMethod} not supported");
            }

            ColorizedMaterialFigure[] figuresForLoad = dataRw.Read();

            foreach(ColorizedMaterialFigure figure in figuresForLoad)
            {
                AddFigure(figure);
            }
        }

        private ColorizedMaterialFigure[] SelectAppropriateFigures(SaveMode saveMode)
        {
            if(saveMode == SaveMode.SaveAllFigures)
            {
                return colorizedMaterialFigures;
            }
            else
            {
                List<ColorizedMaterialFigure> selectedFigures = new List<ColorizedMaterialFigure>();

                for(int i = 0; i < AmountOfFigures; i++)
                {
                    switch (saveMode)
                    {
                        case SaveMode.SaveOnlyFilmFigures:
                            {
                                if (this[i].ColoratedMaterial is Film)
                                {
                                    selectedFigures.Add(this[i]);
                                }
                                break;
                            }
                        case SaveMode.SaveOnlyPaperFigures:
                            {
                                if (this[i].ColoratedMaterial is Paper)
                                {
                                    selectedFigures.Add(this[i]);
                                }
                                break;
                            }
                        default:
                            throw new Exception($"Save mode {saveMode} not supported");
                    }
                }

                return selectedFigures.ToArray();
            }
        }
    }
}
