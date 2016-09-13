using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.WebApi
{
    public class Product
    {
        private int id;
        private string name;
        private string category;
        private decimal price;

        public Product(int Id, string Name, string Category, decimal Price)
        {
            this.id = Id;
            this.name = Name;
            this.category = Category;
            this.price = Price;
        }

        public int Id
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
            }
        }

        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }

        public string Category
        {
            get
            {
                return this.category;
            }
            set
            {
                this.category = value;
            }
        }

        public decimal Price
        {
            get
            {
                return this.price;
            }
            set
            {
                this.price = value;
            }
        }
    }
}