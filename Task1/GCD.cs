using System;

namespace Task1
{
    public static class GCD
    {
        /// <summary>
        /// Calculates greatest common divisor (GCD) of two numbers
        /// using Euclid's alghorithm
        /// </summary>
        /// <param name="a">1st number</param>
        /// <param name="b">2nd number</param>
        /// <returns></returns>
        public static int EuclidGCD(int a, int b)
        {
            int divRemainder;
            do
            {
                if(a < b)
                {
                    Swap(ref a, ref b);
                }
                divRemainder = a % b;
                if(divRemainder != 0)
                {
                    a = divRemainder;
                }
            }
            while (divRemainder != 0);
            return b;
        }

        /// <summary>
        /// Calculates greatest common divisor (GCD) of three numbers
        /// </summary>
        /// <param name="a">1st number</param>
        /// <param name="b">2ns number</param>
        /// <param name="c">3rd number</param>
        /// <returns></returns>
        public static int EuclidGCD(int a, int b, int c) => EuclidGCD(EuclidGCD(a, b), c);

        /// <summary>
        /// Calculates greatest common divisor (GCD) of four numbers
        /// </summary>
        /// <param name="a">1st number</param>
        /// <param name="b">2nd number</param>
        /// <param name="c">3rd number</param>
        /// <param name="d">4th number</param>
        /// <returns></returns>
        public static int EuclidGCD(int a, int b, int c, int d) => EuclidGCD(EuclidGCD(a, b, c), d);


        /// <summary>
        /// Swaps the values of two variables
        /// </summary>
        /// <typeparam name="T">type of swapping values</typeparam>
        /// <param name="a">1st variable for swap</param>
        /// <param name="b">2nd variable for swap</param>
        private static void Swap<T>(ref T a, ref T b)
        {
            T tmp = a;
            a = b;
            b = tmp;
        }
    }
}
