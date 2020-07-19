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

        /// <summary>
        /// method for adding new figures to the box
        /// </summary>
        /// <param name="figure">figure to add</param>
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

        /// <summary>
        /// method for getting a copy of a figure from a box by its number
        /// </summary>
        /// <param name="number">figure number</param>
        /// <returns>copy of figure</returns>
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

        /// <summary>
        /// method for getting a figure by number 
        /// and removing it from the box
        /// </summary>
        /// <param name="number">figure number</param>
        /// <returns>extracted figure</returns>
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

        /// <summary>
        /// method for replacing one figure in a box with another by its number
        /// </summary>
        /// <param name="number">replacement figure number</param>
        /// <param name="replacementFigure">figure to replace</param>
        public void Replace(int number, ColorizedMaterialFigure replacementFigure)
        {
            this[number] = (ColorizedMaterialFigure)replacementFigure.Clone();
        }

        /// <summary>
        /// method for finding a shape in a box similar to a given one
        /// </summary>
        /// <param name="desiredFigure">required figure</param>
        /// <returns>copy of the found figure</returns>
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

        /// <summary>
        /// method for calculating the sum of the areas of all shapes in a box
        /// </summary>
        /// <returns>sum of the areas of all shapes in a box</returns>
        public double CalcTotalArea()
        {
            double area = 0;

            for(int i = 0; i < AmountOfFigures; i++)
            {
                area += this[i].Figure.Area();
            }

            return area;
        }

        /// <summary>
        /// method for calculating the sum of the perimeters of all shapes in a box
        /// </summary>
        /// <returns>sum of the perimeters of all shapes in a box</returns>
        public double CalcTotalPerimeter()
        {
            double perimeter = 0;

            foreach(int i in Enumerable.Range(0, AmountOfFigures))
            {
                perimeter += this[i].Figure.Perimeter();
            }

            return perimeter;
        }

        /// <summary>
        /// method for getting all copies of circles in a box
        /// </summary>
        /// <returns>copies of circles in a box</returns>
        public ColorizedMaterialFigure[] GetAllCircles()
        {
            List<ColorizedMaterialFigure> circles = new List<ColorizedMaterialFigure>();

            for (int i = 0; i < AmountOfFigures; i++)
            {
                if (this[i].Figure is Circle)
                {
                    circles.Add((ColorizedMaterialFigure)this[i].Clone());
                }
            }

            return circles.ToArray();
        }

        /// <summary>
        /// method for getting all copies of film figures in a box
        /// </summary>
        /// <returns>copies of film figures in a box</returns>
        public ColorizedMaterialFigure[] GetAllFilmFigures()
        {
            List<ColorizedMaterialFigure> filmFigures = new List<ColorizedMaterialFigure>();

            for(int i = 0; i < AmountOfFigures; i++)
            {
                if (this[i].ColoratedMaterial is Film)
                {
                    filmFigures.Add((ColorizedMaterialFigure)this[i].Clone());
                }
            }

            return filmFigures.ToArray();
        }

        /// <summary>
        /// method for saving figures from the box to a Xml file
        /// </summary>
        /// <param name="saveMode">save mode (all, only from paper, only from film)</param>
        /// <param name="saveMethod">save method (streamWriter, xmlWriter)</param>
        /// <param name="fileName">filename to save</param>
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

        /// <summary>
        /// method for loading figures to the box from a Xml file
        /// </summary>
        /// <param name="loadMethod">method of loading (streamReader, xmlReader)</param>
        /// <param name="fileName">filename to load</param>
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

        /// <summary>
        /// method for selecting all box figures that satisfy a given parameter
        /// </summary>
        /// <param name="saveMode">save mode (all, only from paper, only from film)</param>
        /// <returns>suitable figures</returns>
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
