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

        public Figure[] Read()
        {
            FigureParser parser = new FigureParser(fileRW.Read());
            return parser.GetFigures();
        }

        public void Write(Figure[] figures)
        {
            StringBuilder sb = new StringBuilder(figures.Length * 10);
            figures.ToList().ForEach(x => sb.Append(x.ToString() + "\n"));
            sb.Remove(sb.Length - 1, 1);
            fileRW.Write(sb.ToString());
        }
    }
}
