using System;

namespace Task1
{
    /// <summary>
    /// class for working with three-dimensional vectors
    /// </summary>
    public class Vector //http://netlib.narod.ru/library/book0032/part1_01.htm
    {
        private double x, y, z;

        public Vector(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        /// <summary>
        /// Get vector length in space (module)
        /// </summary>
        /// <returns>module</returns>
        public double Module() => Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2) + Math.Pow(z, 2));

        /// <summary>
        /// Operation of multiplying a vector by a number
        /// </summary>
        /// <param name="v">vector</param>
        /// <param name="number">number</param>
        /// <returns>multiplied vector</returns>
        public static Vector operator *(Vector v, double number) => new Vector(v.x * number, v.y * number, v.z * number);

        /// <summary>
        /// Operation of dividing a vector by a number
        /// </summary>
        /// <param name="v">vector</param>
        /// <param name="number">number</param>
        /// <returns>divided vector</returns>
        public static Vector operator /(Vector v, double number)
        { 
            if(number == 0)
            {
                throw new DivideByZeroException("can't divide vector by zero");
            }

            return new Vector(v.x / number, v.y / number, v.z / number); 
        }

        /// <summary>
        /// Operation of adding two vectors
        /// </summary>
        /// <param name="u">1st vector</param>
        /// <param name="v">2nd vector</param>
        /// <returns>sum of vectors</returns>
        public static Vector operator +(Vector u, Vector v) => new Vector(u.x + v.x, u.y + v.y, u.z + v.z);

        /// <summary>
        /// Subtraction operation of two vectors
        /// </summary>
        /// <param name="u">1st vector</param>
        /// <param name="v">2nd vector</param>
        /// <returns>vector difference</returns>
        public static Vector operator -(Vector u, Vector v) => new Vector(u.x - v.x, u.y - v.y, u.z - v.z);

        /// <summary>
        /// collinearity check of two vectors
        /// </summary>
        /// <param name="u">1st vector</param>
        /// <param name="v">2nd vector</param>
        /// <returns>is collinear</returns>
        public static bool operator ==(Vector u, Vector v)
        {
            Vector vectorMul = VectorMultiplication(u, v);

            if(vectorMul.x == 0 && vectorMul.y == 0 && vectorMul.z == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// non collinearity check of two vectors
        /// </summary>
        /// <param name="u">1st vector</param>
        /// <param name="v">2nd vector</param>
        /// <returns>is not collinear</returns>
        public static bool operator !=(Vector u, Vector v)
        {
            if(u == v)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// vector sign change operation
        /// </summary>
        /// <param name="v">vector</param>
        /// <returns>vector with changed sign</returns>
        public static Vector operator -(Vector v) => new Vector(-v.x, -v.y, -v.z);

        /// <summary>
        /// calculate scalar multiplication of 2 vectors
        /// </summary>
        /// <param name="u">1st vector</param>
        /// <param name="v">2nd vector</param>
        /// <returns>scalar multiplication of 2 vectors</returns>
        public static Vector ScalarMultiplication(Vector u, Vector v) => new Vector(u.x * v.x, u.y * v.y, u.z * v.z);

        /// <summary>
        /// calculate vector multiplication of 2 vectors
        /// </summary>
        /// <param name="u">1st vector</param>
        /// <param name="v">2nd vector</param>
        /// <returns>vector multiplication of 2 vectors</returns>
        public static Vector VectorMultiplication(Vector u, Vector v)
        {
            double x = u.y * v.z - u.z * v.y;
            double y = u.z * v.x - u.x * v.z;
            double z = u.x * v.y - u.y * v.x;

            return new Vector(x, y, z);
        }

        /// <summary>
        /// Get string representation of the Vector class instance
        /// </summary>
        /// <returns>string representation of a class instance</returns>
        public override string ToString() => $"Vector {x} {y} {z}";

        /// <summary>
        /// Serves as a default hash function
        /// </summary>
        /// <returns>instance hash code</returns>
        public override int GetHashCode()
        {
            return (x.GetHashCode() << 2) ^ (y.GetHashCode() << 1) ^ z.GetHashCode();
        }

        /// <summary>
        /// Determines whether two instances of an object are equal
        /// </summary>
        /// <param name="obj">2nd instance for comparsion</param>
        /// <returns>is equals</returns>
        public override bool Equals(object obj)
        {
            if(obj != null && obj is Vector)
            {
                Vector vector = (Vector)obj;

                if(vector.x == x && vector.y == y && vector.z == z)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
