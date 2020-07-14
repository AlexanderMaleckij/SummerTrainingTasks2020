using System;

namespace Task3
{
    public abstract class Product
    {
        private double price;
        private string name;

        public double Price
        {
            get => price;
            set
            {
                if(value < 0)
                {
                    throw new ArgumentOutOfRangeException("Price can't be negative");
                }
                else
                {
                    price = Math.Round(value, 2);
                }
            }
        }
        public string Name
        {
            get => name;
            set
            {
                if(!string.IsNullOrEmpty(name))
                {
                    name = value;
                }
                else
                {
                    throw new ArgumentException("Name of product must contain at least 1 symbol");
                }
            }
        }

        public Product(string name, double price)
        {
            Name = name;
            Price = price;
        }

        public static implicit operator double(Product product)
        {
            return product.price;
        }

        public static implicit operator int(Product product)
        {
            return (int)(product.price * 100);
        }
    }
}
