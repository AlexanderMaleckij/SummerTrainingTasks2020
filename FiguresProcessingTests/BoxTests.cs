using Microsoft.VisualStudio.TestTools.UnitTesting;
using ColorMaterial;
using Figures;
using System;

namespace FiguresProcessing.Tests
{
    [TestClass()]
    public class BoxTests
    {
        [TestMethod()]
        public void AddFigureTest()
        {
            Box box = new Box();
            ColorizedMaterialFigure[] figures = GenerateDifferentSquares(Box.boxCapacity + 1);

            for(int i = 0; i < Box.boxCapacity - 1; i++)
            {
                box.AddFigure(figures[i]);
            }

            Assert.ThrowsException<ArgumentException>(() => box.AddFigure(figures[0])); //add already existing in box figure

            box.AddFigure(figures[Box.boxCapacity - 1]);    //add the last fitting figure

            Assert.ThrowsException<OverflowException>(() => box.AddFigure(figures[Box.boxCapacity])); //box overflow check
        }

        [TestMethod()]
        public void GetCopyTest()
        {
            ColorizedMaterialFigure figure0 = new ColorizedMaterialFigure(new Rectangle(2, 3), new Film());

            Box box = new Box();
            box.AddFigure(figure0);

            Assert.AreEqual(figure0, box.GetCopy(0));
            Assert.AreNotSame(figure0, box.GetCopy(0));
        }

        [TestMethod()]
        public void ExtractFigureTest()
        {
            ColorizedMaterialFigure figure0 = new ColorizedMaterialFigure(new Rectangle(2, 3), new Film());
            ColorizedMaterialFigure figure1 = new ColorizedMaterialFigure(new Square(3), new Paper());
            ColorizedMaterialFigure figure2 = new ColorizedMaterialFigure(new Square(4), new Film());

            Box box = new Box();
            box.AddFigure(figure0);
            box.AddFigure(figure1);

            box.Replace(0, figure2);

            Assert.AreEqual(figure2, box.GetCopy(0));
        }

        [TestMethod()]
        public void ReplaceTest()
        {
            ColorizedMaterialFigure figure0 = new ColorizedMaterialFigure(new Rectangle(2, 3), new Film());
            ColorizedMaterialFigure figure1 = new ColorizedMaterialFigure(new Square(3), new Paper());
            ColorizedMaterialFigure figure2 = new ColorizedMaterialFigure(new Triangle(3, 4, 5), new Paper());
            ColorizedMaterialFigure replace = new ColorizedMaterialFigure(new Circle(6.2), new Film());

            Box box = new Box();
            box.AddFigure(figure0);
            box.AddFigure(figure1);
            box.AddFigure(figure2);

            box.Replace(1, replace);

            Assert.AreEqual(replace, box.GetCopy(1));
        }

        [TestMethod()]
        public void FindTest()
        {
            ColorizedMaterialFigure figure0 = new ColorizedMaterialFigure(new Rectangle(2, 3), new Film());
            ColorizedMaterialFigure figure1 = new ColorizedMaterialFigure(new Square(3), new Paper());
            ColorizedMaterialFigure figure2 = new ColorizedMaterialFigure(new Triangle(3, 4, 5), new Paper());
            ColorizedMaterialFigure nonAdded = new ColorizedMaterialFigure(new Circle(6.2), new Film());

            Box box = new Box();
            box.AddFigure(figure0);
            box.AddFigure(figure1);
            box.AddFigure(figure2);

            Assert.AreEqual(figure1, box.Find(figure1));
            Assert.AreNotSame(figure1, box.Find(figure1));

            Assert.AreEqual(null, box.Find(nonAdded));
        }

        [TestMethod()]
        public void CalcTotalAreaTest()
        {
            ColorizedMaterialFigure figure0 = new ColorizedMaterialFigure(new Rectangle(2, 3), new Film());
            ColorizedMaterialFigure figure1 = new ColorizedMaterialFigure(new Square(3), new Paper());

            Box box = new Box();
            box.AddFigure(figure0);
            box.AddFigure(figure1);

            Assert.AreEqual(2 * 3 + 3 * 3, box.CalcTotalArea());
        }

        [TestMethod()]
        public void CalcTotalPerimeterTest()
        {
            ColorizedMaterialFigure figure0 = new ColorizedMaterialFigure(new Rectangle(2, 3), new Film());
            ColorizedMaterialFigure figure1 = new ColorizedMaterialFigure(new Square(3), new Paper());

            Box box = new Box();
            box.AddFigure(figure0);
            box.AddFigure(figure1);

            Assert.AreEqual((2 + 3) * 2 + 3 * 4, box.CalcTotalPerimeter());
        }

        [TestMethod()]
        public void GetAllCirclesTest()
        {
            ColorizedMaterialFigure figure0 = new ColorizedMaterialFigure(new Rectangle(2, 3), new Film());
            ColorizedMaterialFigure figure1 = new ColorizedMaterialFigure(new Circle(3.5), new Paper());
            ColorizedMaterialFigure figure2 = new ColorizedMaterialFigure(new Rectangle(2, 4), new Film());
            ColorizedMaterialFigure figure3 = new ColorizedMaterialFigure(new Circle(2), new Paper());
            figure1.ColoratedMaterial.Colorize(Color.Orange);

            Box box = new Box();
            box.AddFigure(figure0);
            box.AddFigure(figure1);
            box.AddFigure(figure2);
            box.AddFigure(figure3);

            ColorizedMaterialFigure[] expected = { figure1, figure3 };
            Assert.IsTrue(IsArraysElementsEquals(expected, box.GetAllCircles()));
        }

        [TestMethod()]
        public void GetAllFilmFiguresTest()
        {
            ColorizedMaterialFigure figure0 = new ColorizedMaterialFigure(new Rectangle(2, 3), new Film());
            ColorizedMaterialFigure figure1 = new ColorizedMaterialFigure(new Triangle(2, 3, 4), new Paper());
            ColorizedMaterialFigure figure2 = new ColorizedMaterialFigure(new Rectangle(2, 4), new Film());
            ColorizedMaterialFigure figure3 = new ColorizedMaterialFigure(new Circle(2), new Paper());
            figure3.ColoratedMaterial.Colorize(Color.Orange);

            Box box = new Box();
            box.AddFigure(figure0);
            box.AddFigure(figure1);
            box.AddFigure(figure2);
            box.AddFigure(figure3);

            ColorizedMaterialFigure[] expected = { figure0, figure2 };
            Assert.IsTrue(IsArraysElementsEquals(expected, box.GetAllFilmFigures()));
        }

        [TestMethod()]
        public void LoadSaveFiguresTest()
        {
            ColorizedMaterialFigure figure0 = new ColorizedMaterialFigure(new Rectangle(2, 3), new Film());
            ColorizedMaterialFigure figure1 = new ColorizedMaterialFigure(new Triangle(2, 3, 4), new Paper());
            ColorizedMaterialFigure figure2 = new ColorizedMaterialFigure(new Rectangle(2, 4), new Film());

            ColorizedMaterialFigure[] colorizedMaterialFigures = { figure0, figure1, figure2 };

            Box box = new Box();
            box.AddFigure(figure0);
            box.AddFigure(figure1);
            box.AddFigure(figure2);

            //test XmlWriter / StreamReader compatibility
            Box box1 = new Box();
            box.SaveFigures(SaveMode.SaveAllFigures, SaveMethod.XmlWriter, "XmlWriterStreamReader.xml");
            box1.LoadFigures(LoadMethod.StreamReader, "XmlWriterStreamReader.xml");
            Assert.IsTrue(IsArraysElementsEquals(colorizedMaterialFigures, new ColorizedMaterialFigure[] { box1.GetCopy(0), box1.GetCopy(1), box1.GetCopy(2) }));

            //test StreamWriter / XmlReader compatibility
            Box box2 = new Box();
            box.SaveFigures(SaveMode.SaveAllFigures, SaveMethod.StreamWriter, "StreamWriterXmlReader.xml");
            box2.LoadFigures(LoadMethod.XmlReader, "StreamWriterXmlReader.xml");
            Assert.IsTrue(IsArraysElementsEquals(colorizedMaterialFigures, new ColorizedMaterialFigure[] { box2.GetCopy(0), box2.GetCopy(1), box2.GetCopy(2) }));

            //Test "save only film figures" save mode
            Box box3 = new Box();
            box.SaveFigures(SaveMode.SaveOnlyFilmFigures, SaveMethod.StreamWriter, "SaveOnlyFilmFiguresTest.xml");
            box3.LoadFigures(LoadMethod.StreamReader, "SaveOnlyFilmFiguresTest.xml");
            Assert.IsTrue(IsArraysElementsEquals(new ColorizedMaterialFigure[] { figure0, figure2}, 
                                                 new ColorizedMaterialFigure[] { box3.GetCopy(0), box3.GetCopy(1) }));

            //Test "save only paper figures" save mode
            Box box4 = new Box();
            box.SaveFigures(SaveMode.SaveOnlyPaperFigures, SaveMethod.XmlWriter, "SaveOnlyPaperFiguresTest.xml");
            box4.LoadFigures(LoadMethod.XmlReader, "SaveOnlyPaperFiguresTest.xml");
            Assert.IsTrue(IsArraysElementsEquals(new ColorizedMaterialFigure[] { figure1, null},
                                                 new ColorizedMaterialFigure[] { box4.GetCopy(0), box4.GetCopy(1) }));
        }

        public bool IsArraysElementsEquals<T>(T[] firstArray, T[] secondArray)
        {
            bool IsHasDifferences = false;

            if(firstArray == null && secondArray == null)
            {
                return true;
            }

            if(firstArray.Length != secondArray.Length)
            {
                return true;
            }

            for(int i = 0; i < firstArray.Length; i++)
            {
                if(firstArray[i] == null && secondArray[i] != null)
                {
                    return false;
                }

                if(firstArray[i] != null && secondArray[i] != null)
                {
                    if (!firstArray[i].Equals(secondArray[i]))
                    {
                        IsHasDifferences = true;
                    }
                }
            }

            return !IsHasDifferences;
        }

        public ColorizedMaterialFigure[] GenerateDifferentSquares(int amount)
        {
            ColorizedMaterialFigure[] figures = new ColorizedMaterialFigure[amount];

            for (int i = 0; i < amount; i++)
            {
                if(i % 2 == 0)
                {
                    figures[i] = new ColorizedMaterialFigure(new Square(i + 1), new Film());
                }
                else
                {
                    figures[i] = new ColorizedMaterialFigure(new Square(i + 1), new Paper());
                }
            }

            return figures;
        }
    }
}