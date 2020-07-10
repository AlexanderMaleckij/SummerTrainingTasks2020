using System.Diagnostics;

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
        public static int EuclidGCD(int a, int b, out long time)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            if(IsGCDBaseCase(a, b, out int c))
            {
                stopwatch.Stop();
                time = stopwatch.ElapsedMilliseconds;
                return c;
            }

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

            stopwatch.Stop();
            time = stopwatch.ElapsedMilliseconds;
            return b;
        }

        /// <summary>
        /// Calculates greatest common divisor (GCD) of three numbers
        /// </summary>
        /// <param name="a">1st number</param>
        /// <param name="b">2ns number</param>
        /// <param name="c">3rd number</param>
        /// <returns></returns>
        public static int EuclidGCD(int a, int b, int c) => EuclidGCD(EuclidGCD(a, b, out _), c, out _);

        /// <summary>
        /// Calculates greatest common divisor (GCD) of four numbers
        /// </summary>
        /// <param name="a">1st number</param>
        /// <param name="b">2nd number</param>
        /// <param name="c">3rd number</param>
        /// <param name="d">4th number</param>
        /// <returns></returns>
        public static int EuclidGCD(int a, int b, int c, int d) => EuclidGCD(EuclidGCD(a, b, c), d, out _);

        /// <summary>
        /// Calculates greatest common divisor (GCD) of two numbers
        /// using Stein's (Binary) algorithm
        /// </summary>
        /// <param name="a">1st mumber</param>
        /// <param name="b">2ns number</param>
        /// <param name="time">algorithm execution time</param>
        /// <returns>GCD</returns>
        public static int SteinGCD(int a, int b, out long time)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            if (IsGCDBaseCase(a, b, out int c))
            {
                stopwatch.Stop();
                time = stopwatch.ElapsedMilliseconds;
                return c;
            }

            int shift = 0;

            /* Let shift := lg K, where K is the greatest power of 2
                dividing both u and v. */
            while (((a | b) & 1) == 0)
            {
                shift++;
                a >>= 1;
                b >>= 1;
            }

            while ((a & 1) == 0)
                a >>= 1;

            /* From here on, u is always odd. */
            do
            {
                /* remove all factors of 2 in v -- they are not common */
                /*   note: v is not zero, so while will terminate */
                while ((b & 1) == 0)
                    b >>= 1;

                /* Now u and v are both odd. Swap if necessary so u <= v,
                    then set v = v - u (which is even). For bignums, the
                     swapping is just pointer movement, and the subtraction
                      can be done in-place. */
                if (a > b)
                {
                    Swap(ref a, ref b);
                }

                b -= a; // Here v >= u.
            } while (b != 0);

            stopwatch.Stop();
            time = stopwatch.ElapsedMilliseconds;
            /* restore common factors of 2 */
            return a << shift;
        }

        /// <summary>
        /// Checks simple case of calculating GCD (use GCD math properties)
        /// </summary>
        /// <param name="a">1st number</param>
        /// <param name="b">2nd number</param>
        /// <param name="c">answer (GCD)</param>
        /// <returns>true - found answer</returns>
        private static bool IsGCDBaseCase(int a, int b, out int c)
        {
            if(a == b)  //GCD(x; x) = x
            {
                c = a;
                return true;
            }
            if(a == 0)  //GCD(0; x) = x;
            {
                c = b;
                return true;
            }
            if(b == 0)  //GCD(x; 0) = x;
            {
                c = a;
                return true;
            }

            c = default;
            return false;
        }

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
