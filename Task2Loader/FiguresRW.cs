using System.Linq;
using System.Text;
using Task2Figures;

namespace Task2Loader
{
    public class FiguresRW
    {
        IDataRW fileRW;

        public FiguresRW(string fileName)
        {
            fileRW = new FileRW(fileName);
        }

        /// <summary>
        /// Read figures from file to array
        /// </summary>
        /// <returns>array of Figures</returns>
        public Figure[] Read()
        {
            FigureParser parser = new FigureParser(fileRW.Read());
            return parser.GetFigures();
        }

        /// <summary>
        /// Write figures from array to file
        /// </summary>
        /// <param name="figures">figures for writing</param>
        public void Write(Figure[] figures)
        {
            StringBuilder sb = new StringBuilder(figures.Length * 10);
            figures.ToList().ForEach(x => sb.Append(x.ToString() + "\n"));
            sb.Remove(sb.Length - 1, 1);
            fileRW.Write(sb.ToString());
        }
    }
}
