﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Figures;
using ColorMaterial;

namespace DataRW.Tests
{
    [TestClass()]
    public class StreamDataRWXmlTests
    {
        [TestMethod()]
        public void StreamDataRWXmlTest()
        {
            IDataRW dataRW = new StreamDataRWXml("StreamDataRWXmlTest.xml");
            Assert.IsNotNull(dataRW);
        }

        [TestMethod()]
        public void ReadWriteTest()
        {
            ColorizedMaterialFigure colorizedMaterialFigure = new ColorizedMaterialFigure(new Square(2.2), new Paper());
            colorizedMaterialFigure.ColoratedMaterial.Colorize(Color.Green);

            ColorizedMaterialFigure[] expectedArray = {
                new ColorizedMaterialFigure(new Rectangle(2, 5), new Film()),
                new ColorizedMaterialFigure(new Triangle(2.5, 3.5, 4.5), new Paper()),
                colorizedMaterialFigure
            };

            IDataRW dataRW = new StreamDataRWXml("StreamDataRWXmlTest.xml");

            dataRW.Write(expectedArray);

            ColorizedMaterialFigure[] actualArray = dataRW.Read();

            Assert.AreEqual(expectedArray.Length, actualArray.Length);

            for (int i = 0; i < expectedArray.Length; i++)
            {
                Assert.AreEqual(expectedArray[i], actualArray[i]);
            }
        }
    }
}