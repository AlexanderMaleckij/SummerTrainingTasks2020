using System;
using System.Collections.Generic;
using System.Linq;
using Task2Figures;

namespace Task2Loader
{
    class FigureParser
    {
        private string content;
        private List<string> FilledContentLines
        {
            get
            {
                List<string> lines = content.Split('\n').ToList();
                return lines.Where(x => !string.IsNullOrEmpty(x)).ToList();
            }
        }

        public FigureParser(string content)
        {
            this.content = content;
        }

        /// <summary>
        /// Create Figure instances from a string passed 
        /// when initializing an instance of this class
        /// </summary>
        /// <returns>array of converted Figures</returns>
        public Figure[] GetFigures()
        {
            List<Figure> parsedFigures = new List<Figure>();

            foreach (string line in FilledContentLines)
            {
                Figure figure = ParseFigure(line);
                parsedFigures.Add(figure);
            }
            return parsedFigures.ToArray();
        }

        /// <summary>
        /// Get Figure instance from its string representation
        /// </summary>
        /// <param name="figureStr">string representation of Figure instance</param>
        /// <returns>Figure instance of given string</returns>
        private static Figure ParseFigure(string figureStr)
        {
            string figureType = figureStr.Split(' ').First();
            List<double> constructorParams = Convert(figureStr.Split(' ').Skip(1).ToList());

            switch(figureType)
            {
                case "Circle":
                    if(constructorParams.Count == 1)
                    {
                        return new Circle(constructorParams[0]);
                    }
                    break;
                case "Triangle":
                    if (constructorParams.Count == 3)
                    {
                        return new Triangle(constructorParams[0], constructorParams[1], constructorParams[2]);
                    }
                    break;
                case "Square":
                    if (constructorParams.Count == 1)
                    {
                        return new Square(constructorParams[0]);
                    }
                    break;
                case "Rectangle":
                    if (constructorParams.Count == 2)
                    {
                        return new Rectangle(constructorParams[0], constructorParams[1]);
                    }
                    break;
                default:
                    throw new Exception($"{figureType} not found");
            }
            throw new ArgumentException("Тumber of parameters does not match the number of constructor parameters");
        }

        /// <summary>
        /// Convert each string value from given list to double
        /// </summary>
        /// <param name="items">items for convertation</param>
        /// <returns>list of double values</returns>
        private static List<double> Convert(List<string> items)
        {
            List<double> itemsDouble = new List<double>();
            items.ForEach(x => itemsDouble.Add(double.Parse(x)));
            return itemsDouble;
        }
    }
}
