using System;

namespace Task3
{
    /// <summary>
    /// Class, that represent all scales products
    /// </summary>
    public class Scales : HouseholdAppliances
    {
        private int maxWeightGrams;
        private int accuracyPercents;

        public int MaxWeightGrams
        {
            get => maxWeightGrams;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Maximum measured weight can't be less than 0");
                }
                else
                {
                    maxWeightGrams = value;
                }
            }
        }

        public int AccuracyPercents
        {
            get => accuracyPercents;
            set
            {
                if (value < 0 || value > 100)
                {
                    throw new ArgumentException("Scales accuracy can't be less than 0% and greater than 100%");
                }
                else
                {
                    accuracyPercents = value;
                }
            }
        }


        public Scales(string modelName, double price) : base(modelName, price) { }

        public Scales(string modelName, double price, int maxWeightGrams, int accuracyPercents) : base(modelName, price)
        {
            MaxWeightGrams = maxWeightGrams;
            AccuracyPercents = accuracyPercents;
        }

        public static Scales operator +(Scales s1, Scales s2)
        {
            return new Scales($"{s1.Name} - {s2.Name}", (s1.Price + s2.Price) / 2);
        }

        #region explicit castings to other types of products
        /// <summary>
        /// Explicit casting of product with type Scales to type BreadMachine
        /// </summary>
        /// <param name="scales">Instance of the Scales class for casting</param>
        public static explicit operator BreadMachine(Scales scales)
        {
            return new BreadMachine(scales.Name, scales.Price);
        }

        /// <summary>
        /// Explicit casting of product with type Scales to type Laptop
        /// </summary>
        /// <param name="scales">Instance of the Scales class for casting</param>
        public static explicit operator Laptop(Scales scales)
        {
            return new Laptop(scales.Name, scales.Price);
        }

        /// <summary>
        /// Explicit casting of product with type Scales to type Monitor
        /// </summary>
        /// <param name="scales">Instance of the Scales class for casting</param>
        public static explicit operator Monitor(Scales scales)
        {
            return new Monitor(scales.Name, scales.Price);
        }
        #endregion
    }
}
