using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.WebApi
{
    public class Product : TableEntity
    {
        private int id;
        private string name;
        private string category;
        //private decimal price;
        private double price;

        public Product() { }

        //public Product(int Id, string Name, string Category, decimal Price)
        public Product(int Id, string Name, string Category, double Price)
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

        //public decimal Price
        public double Price
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