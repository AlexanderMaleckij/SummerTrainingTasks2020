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
        
        private static void Swap<T>(ref T a, ref T b)
        {
            T tmp = a;
            a = b;
            b = tmp;
        }
    }
}
