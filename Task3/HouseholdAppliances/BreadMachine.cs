using System;

namespace Task3
{
    /// <summary>
    /// Class, that represent all bread machine products
    /// </summary>
    public class BreadMachine : HouseholdAppliances
    {
        public bool IsHasNonStickCoating { get; set; }
        private int powerConsumption;
        private int maxBuckingWeightGrams;
        
        public int PowerConsumption
        {
            get => powerConsumption;
            set
            {
                if(value < 0)
                {
                    throw new ArgumentException("Power consumption of bread machine can't be less than 0");
                }
                else
                {
                    powerConsumption = value;
                }
            }
        }
        public int MaxBuckingWeightGrams
        {
            get => maxBuckingWeightGrams;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Maximum bucking weight can't be less than 0");
                }
                else
                {
                    maxBuckingWeightGrams = value;
                }
            }
        }

        public BreadMachine(string modelName, double price) : base(modelName, price) { }

        public BreadMachine(string modelName, double price, bool isHasNonStickCoating, int powerConsumption, int maxBuckingWeightGrams) : base(modelName, price)
        {
            IsHasNonStickCoating = isHasNonStickCoating;
            PowerConsumption = powerConsumption;
            MaxBuckingWeightGrams = maxBuckingWeightGrams;
        }

        public static BreadMachine operator +(BreadMachine bm1, BreadMachine bm2)
        {
            return new BreadMachine($"{bm1.Name} - {bm2.Name}", (bm1.Price + bm2.Price) / 2);
        }

        #region explicit castings to other types of products
        /// <summary>
        /// Explicit casting of product with type BreadMachine to type Scales
        /// </summary>
        /// <param name="breadMachine"></param>
        public static implicit operator Scales(BreadMachine breadMachine)
        {
            return new Scales(breadMachine.Name, breadMachine.Price);
        }

        /// <summary>
        /// Explicit casting of product with type BreadMachine to type Laptop
        /// </summary>
        /// <param name="breadMachine"></param>
        public static implicit operator Laptop(BreadMachine breadMachine)
        {
            return new Laptop(breadMachine.Name, breadMachine.Price);
        }

        /// <summary>
        /// Explicit casting of product with type BreadMachine to type Monitor
        /// </summary>
        /// <param name="breadMachine"></param>
        public static implicit operator Monitor(BreadMachine breadMachine)
        {
            return new Monitor(breadMachine.Name, breadMachine.Price);
        }
        #endregion
    }
}
