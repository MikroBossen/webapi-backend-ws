using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Backend.WebApi
{
    public class ProductsController : ApiController
    {
        private Product[] products = new Product[] { new Product(101, "Handbag", "Accessories", 12.50m),
                                                     new Product(207, "Shirt", "Clothes", 5.25m),
                                                     new Product(315, "Leather boots", "Shoes", 25.00m),
                                                     new Product(108, "Necklace", "Accessories", 51.50m),
                                                     new Product(202, "Shorts", "Clothes", 7.95m) };

        public IEnumerable<Product> GetAllProducts()
        {
            return this.products;
        }

        public IHttpActionResult GetProduct(int id)
        {
            for (int i = 0; i < products.Length; i++)
            {
                if (products[i].Id == id)
                {
                    return Ok(products[i]);
                }
            }
            return NotFound();
        }
    }
}
