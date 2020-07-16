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
                    throw new ArgumentException("Price can't be negative");
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
                if(!string.IsNullOrEmpty(value))
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

        public static explicit operator double(Product product)
        {
            return product.price;
        }

        public static explicit operator int(Product product)
        {
            return (int)(product.price * 100);
        }
    }
}
