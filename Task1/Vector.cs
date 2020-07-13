using System;

namespace Task1
{
    public class Vector //http://netlib.narod.ru/library/book0032/part1_01.htm
    {
        private double x, y, z;

        public Vector(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public double Module() => Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2) + Math.Pow(z, 2));

        public static Vector operator *(Vector v, double number) => new Vector(v.x * number, v.y * number, v.z * number);

        public static Vector operator /(Vector v, double number)
        { 
            if(number == 0)
            {
                throw new DivideByZeroException("can't divide vector by zero");
            }

            return new Vector(v.x / number, v.y / number, v.z / number); 
        }

        public static Vector operator +(Vector u, Vector v) => new Vector(u.x + v.x, u.y + v.y, u.z + v.z);

        public static Vector operator -(Vector u, Vector v) => new Vector(u.x - v.x, u.y - v.y, u.z - v.z);

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

        public static Vector operator -(Vector v) => new Vector(-v.x, -v.y, -v.z);

        public static Vector ScalarMultiplication(Vector u, Vector v) => new Vector(u.x * v.x, u.y * v.y, u.z * v.z);

        public static Vector VectorMultiplication(Vector u, Vector v)
        {
            double x = u.y * v.z - u.z * v.y;
            double y = u.z * v.x - u.x * v.z;
            double z = u.x * v.y - u.y * v.x;

            return new Vector(x, y, z);
        }

        public override string ToString() => $"Vector {x} {y} {z}";

        public override int GetHashCode()
        {
            return (x.GetHashCode() << 2) ^ (y.GetHashCode() << 1) ^ z.GetHashCode();
        }

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
