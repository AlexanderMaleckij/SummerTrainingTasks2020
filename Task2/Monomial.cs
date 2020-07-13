
namespace Task2
{
    public class Monomial
    {
        public int xPower;
        public double coefficient;

        public Monomial(int xPower, double coefficient)
        {
            this.xPower = xPower;
            this.coefficient = coefficient;
        }

        public override string ToString()
        {
            if(coefficient == 0)
            {
                return string.Empty;
            }
            else
            {
                if(xPower == 0)
                {
                    return coefficient.ToString();
                }
                else
                {
                    if (xPower > 0)
                    {
                        return $"{coefficient}*x^{xPower}";
                    }
                    else
                    {
                        return $"{coefficient}*x^({xPower})";
                    }
                }
            }
        }
        public override bool Equals(object obj)
        {
            if (obj != null && obj is Monomial)
            {
                Monomial monomial = (Monomial)obj;

                if(xPower == monomial.xPower && coefficient == monomial.coefficient)
                {
                    return true;
                }
            }

            return false;
        }

        public override int GetHashCode()
        {
            return (xPower.GetHashCode() << 1) ^ coefficient.GetHashCode();
        }
    }
}
