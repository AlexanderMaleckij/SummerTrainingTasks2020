using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
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

        private static List<double> Convert(List<string> items)
        {
            List<double> itemsDouble = new List<double>();
            items.ForEach(x => itemsDouble.Add(double.Parse(x)));
            return itemsDouble;
        }
    }
}
