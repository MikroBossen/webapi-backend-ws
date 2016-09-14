using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.WebApi.Models
{
    public class Review
    {
        private int id;
        private int productId;
        private int rating;
        private string text;

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

        public int ProductId
        {
            get
            {
                return this.productId;
            }
            set
            {
                this.productId = value;
            }
        }

        public int Rating
        {
            get
            {
                return this.rating;
            }
            set
            {
                this.rating = value;
            }
        }

        public string Text
        {
            get
            {
                return this.text;
            }
            set
            {
                this.text = value;
            }
        }
    }
}