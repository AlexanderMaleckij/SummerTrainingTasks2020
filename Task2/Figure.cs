
namespace Task2Figures
{
    public abstract class Figure
    {
        protected const string negativeSideLengthMsg = "length of the side can't be negative";
        public abstract double Area();
        public abstract double Perimeter();
    }
}
