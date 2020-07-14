using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace Task3
{
    public abstract class ProductType : Product
    {
        private string type;
        public string Type
        {
            get => type;
            set
            {
                if (!string.IsNullOrEmpty(type))
                {
                    type = value;
                }
                else
                {
                    throw new ArgumentException("Name of product must contain at least 1 symbol");
                }
            }
        }

        public ProductType(string name, string type, double price) : base(name, price)
        {
            Type = type;
        }
    }
}
