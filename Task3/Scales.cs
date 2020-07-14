using System;

namespace Task3
{
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
    }
}
