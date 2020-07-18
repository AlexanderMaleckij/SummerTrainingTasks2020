using ColorMaterial;
using Figures;
using FiguresProcessing;
using System.Xml;

namespace DataRW
{
    class XmlDataRWXml : IDataRW
    {
        private string fileName;

        public XmlDataRWXml(string fileName)
        {
            this.fileName = fileName;
        }

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

        public void Write(ColorizedMaterialFigure[] figures)
        {
            XmlWriter xmlWriter = XmlWriter.Create(fileName);
            xmlWriter.WriteStartElement("ColorizedMaterialFigures");
            foreach (ColorizedMaterialFigure arrayFigure in figures)
            {
                xmlWriter.WriteStartElement("ColorizedMaterialFigure");
                WriteColoratedMaterial(arrayFigure.ColoratedMaterial, xmlWriter);
                WriteFigure(arrayFigure.Figure, xmlWriter);
                xmlWriter.WriteEndElement();
            }
            xmlWriter.WriteEndAttribute();
        }

        private static void WriteColoratedMaterial(ColoratedMaterial coloratedMaterial, XmlWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("ColoratedMaterial");
            string[] coloratedMaterialFields = coloratedMaterial.ToString().Split(' ');
            WriteElement("Color", coloratedMaterialFields[0], xmlWriter);
            WriteElement("Material", coloratedMaterialFields[1], xmlWriter);
            xmlWriter.WriteEndElement();
        }

        private static void WriteFigure(Figure figure, XmlWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("Figure");
            string[] figureFields = figure.ToString().Split(' ');
            WriteElement("Type", figureFields[0], xmlWriter);
            for (int i = 1; i < figureFields.Length; i++)
            {
                WriteElement($"{i}SideLength", figureFields[i], xmlWriter);
            }
            xmlWriter.WriteEndElement();
        }

        private static void WriteElement(string name, string value, XmlWriter xmlWriter)
        {
            xmlWriter.WriteStartElement(name);
            xmlWriter.WriteString(value);
            xmlWriter.WriteEndElement();
        }
    }
}
