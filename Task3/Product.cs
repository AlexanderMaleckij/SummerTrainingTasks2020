using System;

namespace Task3
{
    /// <summary>
    /// Base class for all concrete products
    /// </summary>
    public abstract class Product
    {
        private double price;
        private string name;

        /// <summary>
        /// Auto property of product price
        /// </summary>
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

        /// <summary>
        /// Auto property of product name (or in another way the product model)
        /// </summary>
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

        /// <summary>
        /// Casting product class inheritor instance to it's floating cost value
        /// </summary>
        /// <param name="product">product price in floating point value</param>
        public static explicit operator double(Product product)
        {
            return product.price;
        }

        /// <summary>
        /// Casting product class inheritor instance to it's integer cost value
        /// </summary>
        /// <param name="product">product price in integer value</param>
        public static explicit operator int(Product product)
        {
            return (int)(product.price * 100);
        }
    }
}
