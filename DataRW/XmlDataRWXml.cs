using ColorMaterial;
using Figures;
using System.Xml;

namespace DataRW
{
    /// <summary>
    /// Class for read and write ColorizedMaterialFigure class instances
    /// to xml file using XmlReader and XmlWriter
    /// </summary>
    public class XmlDataRWXml : IDataRW
    {
        private string fileName;

        public XmlDataRWXml(string fileName)
        {
            this.fileName = fileName;
        }

        /// <summary>
        /// Method for reading figures from Xml file using XmlReader
        /// </summary>
        /// <returns>readed figures</returns>
        public ColorizedMaterialFigure[] Read()
        {
            XmlReaderSettings settings = new XmlReaderSettings();
            XmlDocument xmlDocument = new XmlDocument();

            using (XmlReader reader = XmlReader.Create(fileName, settings))
            {
                xmlDocument = new XmlDocument();
                xmlDocument.Load(reader);
            }

            return StreamDataRWXml.GetFiguresFromXMLDocument(xmlDocument);
        }

        /// <summary>
        /// Method for writing figures to Xml file using XmlWriter
        /// </summary>
        /// <param name="figures">figures for writing</param>
        public void Write(ColorizedMaterialFigure[] figures)
        {
            XmlWriter xmlWriter = XmlWriter.Create(fileName);
            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("ColorizedMaterialFigures");
            foreach (ColorizedMaterialFigure arrayFigure in figures)
            {
                if(arrayFigure != null)
                {
                    xmlWriter.WriteStartElement("ColorizedMaterialFigure");
                    WriteColoratedMaterial(arrayFigure.ColoratedMaterial, xmlWriter);
                    WriteFigure(arrayFigure.Figure, xmlWriter);
                    xmlWriter.WriteEndElement();
                }
            }
            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndDocument();
            xmlWriter.Flush();
            xmlWriter.Close();
            xmlWriter.Dispose();
        }

        /// <summary>
        /// Method for writing given ColoratedMaterial class instance using given xmlWriter
        /// </summary>
        /// <param name="coloratedMaterial">instance for writing</param>
        /// <param name="xmlWriter">xml writer</param>
        private static void WriteColoratedMaterial(ColoratedMaterial coloratedMaterial, XmlWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("ColoratedMaterial");
            string[] coloratedMaterialFields = coloratedMaterial.ToString().Split(' ');
            WriteElement("Color", coloratedMaterialFields[0], xmlWriter);
            WriteElement("Material", coloratedMaterialFields[1], xmlWriter);
            xmlWriter.WriteEndElement();
        }

        /// <summary>
        /// Method for writing given Figure class instance using given xmlWriter
        /// </summary>
        /// <param name="figure">figure for writing</param>
        /// <param name="xmlWriter">xml writer</param>
        private static void WriteFigure(Figure figure, XmlWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("Figure");
            string[] figureFields = figure.ToString().Split(' ');
            WriteElement("Type", figureFields[0], xmlWriter);
            for (int i = 1; i < figureFields.Length; i++)
            {
                WriteElement("SideLength", figureFields[i], xmlWriter);
            }
            xmlWriter.WriteEndElement();
        }

        /// <summary>
        /// Method for writing xml element to file
        /// </summary>
        /// <param name="name">xml element name</param>
        /// <param name="value">xml element content</param>
        /// <param name="xmlWriter">xml writer</param>
        private static void WriteElement(string name, string value, XmlWriter xmlWriter)
        {
            xmlWriter.WriteStartElement(name);
            xmlWriter.WriteString(value);
            xmlWriter.WriteEndElement();
        }
    }
}
