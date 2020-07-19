using ColorMaterial;
using Figures;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace DataRW
{
    /// <summary>
    /// Class for read and write ColorizedMaterialFigure class instances
    /// to xml file using StreamReader and StreamWriter
    /// </summary>
    public class StreamDataRWXml : IDataRW
    {
        private string fileName;
        public StreamDataRWXml(string fileName)
        {
            this.fileName = fileName;
        }

        /// <summary>
        /// Method for reading figures from Xml file using StreamReader
        /// </summary>
        /// <returns></returns>
        public ColorizedMaterialFigure[] Read()
        {
            XmlDocument xmlDocument = new XmlDocument();
            using (StreamReader stream = new StreamReader(fileName, Encoding.UTF8))
            {
                xmlDocument.Load(stream);
            }

            return GetFiguresFromXMLDocument(xmlDocument);
        }

        /// <summary>
        /// Method for writing figures to Xml file using StreamWriter
        /// </summary>
        /// <param name="figures"></param>
        public void Write(ColorizedMaterialFigure[] figures)
        {
            XmlDocument xmlDocument = GetXMLDocument(figures);

            using (StreamWriter sw = new StreamWriter(fileName, false, Encoding.UTF8))
            {
                xmlDocument.Save(sw);
            }
        }

        /// <summary>
        /// Method for converting given xmlDocument to array of figures
        /// </summary>
        /// <param name="xmlDocument">xml document</param>
        /// <returns>array of figures</returns>
        public static ColorizedMaterialFigure[] GetFiguresFromXMLDocument(XmlDocument xmlDocument)
        {
            List<ColorizedMaterialFigure> colorizedMaterialFigures = new List<ColorizedMaterialFigure>();
            XmlElement root = xmlDocument.DocumentElement;
            Assembly colorMaterialAssembly = Assembly.Load("ColorMaterial");
            Assembly figuresAssembly = Assembly.Load("Figures");

            foreach (XmlNode xnode in root)
            {
                ColoratedMaterial coloratedMaterial = null;
                Figure figure = null;

                foreach (XmlNode childnode in xnode.ChildNodes)
                {
                    if (childnode.Name == "ColoratedMaterial")
                    {
                        string color    = string.Empty; 
                        string material = string.Empty;

                        if(childnode.ChildNodes[0].Name == "Color")
                        {
                            color = childnode.ChildNodes[0].InnerText;
                        }

                        if (childnode.ChildNodes[1].Name == "Material")
                        {
                            material = childnode.ChildNodes[1].InnerText;
                        }
                        
                        Type materialType = colorMaterialAssembly.GetType("ColorMaterial." + material, true, true);
                        ConstructorInfo materialTypeConstructor = materialType.GetConstructor(new Type[] { });
                        coloratedMaterial = (ColoratedMaterial)materialTypeConstructor.Invoke(new object[] { });

                        if(coloratedMaterial is Paper && color != Paper.defaultPaperColor.ToString())
                        {
                            coloratedMaterial.Colorize((Color)Enum.Parse(typeof(Color), color));
                        }
                    }

                    if (childnode.Name == "Figure")
                    {
                        int constructorParamsAmount = childnode.ChildNodes.Count - 1;
                        Type figureType = figuresAssembly.GetType("Figures." + childnode.ChildNodes[0].InnerText, true, true);

                        switch (constructorParamsAmount)
                        {
                            case 1:
                                ConstructorInfo constructorInfo = figureType.GetConstructor(new Type[] { typeof(double) });
                                figure = (Figure)constructorInfo.Invoke(new object[] { double.Parse(childnode.ChildNodes[1].InnerText) } );
                                break;
                            case 2:
                                constructorInfo = figureType.GetConstructor(new Type[] { typeof(double), typeof(double) });
                                figure = (Figure)constructorInfo.Invoke(new object[] { double.Parse(childnode.ChildNodes[1].InnerText), 
                                                                                       double.Parse(childnode.ChildNodes[2].InnerText) });
                                break;
                            case 3:
                                constructorInfo = figureType.GetConstructor(new Type[] { typeof(double), typeof(double), typeof(double) });
                                figure = (Figure)constructorInfo.Invoke(new object[] { double.Parse(childnode.ChildNodes[1].InnerText),
                                                                                       double.Parse(childnode.ChildNodes[2].InnerText),
                                                                                       double.Parse(childnode.ChildNodes[3].InnerText)});
                                break;
                        }
                    }
                }
                colorizedMaterialFigures.Add(new ColorizedMaterialFigure(figure, coloratedMaterial));
            }
            return colorizedMaterialFigures.ToArray();
        }

        /// <summary>
        /// Method for getting a xmlDocument from given array of figures
        /// </summary>
        /// <param name="figures">figures</param>
        /// <returns>xml document</returns>
        private static XmlDocument GetXMLDocument(ColorizedMaterialFigure[] figures)
        {
            XElement rootElement = new XElement("ColorizedMaterialFigures");

            foreach (ColorizedMaterialFigure arrayFigure in figures)
            {
                if(arrayFigure != null)
                {
                    rootElement.Add(
                    new XElement("ColorizedMaterialFigure",
                    ConvertToXElement(arrayFigure.ColoratedMaterial),
                    ConvertToXElement(arrayFigure.Figure))
                    );
                }
            }

            XDocument xdocument = new XDocument();
            xdocument.Add(rootElement);

            return ConvertToXmlDocument(xdocument);
        }

        /// <summary>
        /// Method for getting a XElement from given ColoratedMaterial class instance
        /// </summary>
        /// <param name="coloratedMaterial">ColoratedMaterial class instance</param>
        /// <returns>XElement of ColoratedMaterial class instance</returns>
        private static XElement ConvertToXElement(ColoratedMaterial coloratedMaterial)
        {
            string[] coloratedMaterialFields = coloratedMaterial.ToString().Split(' ');
            return new XElement("ColoratedMaterial",
                   new XElement("Color",    coloratedMaterialFields[0]),
                   new XElement("Material", coloratedMaterialFields[1]));
        }

        /// <summary>
        /// Method for converting given Figure class instance to XElement
        /// </summary>
        /// <param name="figure">figure</param>
        /// <returns>XElement of figure</returns>
        private static XElement ConvertToXElement(Figure figure)
        {
            string[] figureFiels = figure.ToString().Split(' ');
            XElement convertedFigure = new XElement("Figure");
            convertedFigure.Add(new XElement("Type", figureFiels[0]));
            for (int i = 1; i < figureFiels.Length; i++)
            {
                convertedFigure.Add(new XElement("SideLength", figureFiels[i]));
            }
            return convertedFigure;
        }

        /// <summary>
        /// Method for converting a instance of type XDocument to XmlDocument
        /// </summary>
        /// <param name="xDocument">xDocument instance</param>
        /// <returns>XmlDocument instance of xDocument instance</returns>
        private static XmlDocument ConvertToXmlDocument(XDocument xDocument)
        {
            XmlDocument xmlDocument = new XmlDocument();

            using (XmlReader xmlReader = xDocument.CreateReader())
            {
                xmlDocument.Load(xmlReader);
            }

            return xmlDocument;
        }
    }
}
