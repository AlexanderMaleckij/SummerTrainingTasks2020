using System;

namespace Task3
{
    /// <summary>
    /// Class, that represent all monitors products
    /// </summary>
    public class Monitor : ComputerTechnics
    {
        private double diagonalInches;
        private string matrixType;
        private string resolution;
        public double DiagonalInches
        {
            get => diagonalInches;
            set
            {
                if (value < 10)
                {
                    throw new ArgumentException("Laptop diagonal can't be less than 10\"");
                }
                else
                {
                    diagonalInches = value;
                }
            }
        }

        public string MatrixType
        {
            get => matrixType;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new Exception("Type of matrix can't be empty");
                }
                else
                {
                    matrixType = value;
                }
            }
        }

        public string Resolution
        {
            get => resolution;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new Exception("Resolution can't be empty");
                }
                else
                {
                    resolution = value;
                }
            }
        }

        public Monitor(string modelName, double price) : base(modelName, price) { }

        public Monitor(string modelName, double price, double diagonalInches, string matrixType, string resolution) : base(modelName, price)
        {
            DiagonalInches = diagonalInches;
            MatrixType = matrixType;
            Resolution = resolution;
        }

        public static Monitor operator +(Monitor m1, Monitor m2)
        {
            return new Monitor($"{m1.Name} - {m2.Name}", (m1.Price + m2.Price) / 2);
        }

        #region explicit castings to other types of products
        /// <summary>
        /// Explicit casting of product with type Monitor to type Scales
        /// </summary>
        /// <param name="monitor">Instance of the Montior class for casting</param>
        public static explicit operator Scales(Monitor monitor)
        {
            return new Scales(monitor.Name, monitor.Price);
        }

        /// <summary>
        /// Explicit casting of product with type Monitor to type BreadMachine
        /// </summary>
        /// <param name="monitor">Instance of the Montior class for casting</param>
        public static explicit operator BreadMachine(Monitor monitor)
        {
            return new BreadMachine(monitor.Name, monitor.Price);
        }

        /// <summary>
        /// Explicit casting of product with type Monitor to type Laptop
        /// </summary>
        /// <param name="monitor">Instance of the Montior class for casting</param>
        public static explicit operator Laptop(Monitor monitor)
        {
            return new Laptop(monitor.Name, monitor.Price);
        }
        #endregion
    }
}
