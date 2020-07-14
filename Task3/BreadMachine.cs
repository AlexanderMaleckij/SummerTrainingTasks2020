using System;

namespace Task3
{
    class BreadMachine : HouseholdAppliances
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
    }
}
