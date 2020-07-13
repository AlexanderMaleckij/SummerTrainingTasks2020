
namespace Task2
{
    /// <summary>
    /// An instance of this class may be part of a polynomial
    /// </summary>
    public class Monomial
    {
        public double xPower;
        public double coefficient;

        public Monomial(double xPower, double coefficient)
        {
            this.xPower = xPower;
            this.coefficient = coefficient;
        }

        /// <summary>
        /// Get string representation of the Mononial class instance
        /// </summary>
        /// <returns>string representation of a class instance</returns>
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

        /// <summary>
        /// Determines whether two instances of an object are equal
        /// </summary>
        /// <param name="obj">2nd instance for comparsion</param>
        /// <returns>is equals</returns>
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


        /// <summary>
        /// Serves as a default hash function
        /// </summary>
        /// <returns>instance hash code</returns>
        public override int GetHashCode()
        {
            return (xPower.GetHashCode() << 1) ^ coefficient.GetHashCode();
        }
    }
}
