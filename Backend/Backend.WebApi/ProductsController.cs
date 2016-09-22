using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Backend.WebApi
{
    public class ProductsController : ApiController
    {
        // Orig. løsning, hvor price var en decimal
        //private Product[] products = new Product[] { new Product(101, "Handbag", "Accessories", 12.50m) { PartitionKey = "part1", RowKey = "1" },
        //                                             new Product(207, "Shirt", "Clothes", 5.25m) { PartitionKey = "part1", RowKey = "2" },
        //                                             new Product(315, "Leather boots", "Shoes", 25.00m)  { PartitionKey = "part1", RowKey = "3" },
        //                                             new Product(108, "Necklace", "Accessories", 51.50m)  { PartitionKey = "part1", RowKey = "4" },
        //                                             new Product(202, "Shorts", "Clothes", 7.95m)  { PartitionKey = "part1", RowKey = "5" } };

        // Azure storage, hvor decimal ikke er tilladt som datatype og price derfor skal ændres til en double
        private Product[] products = new Product[] { new Product(101, "Handbag", "Accessories", 12.50) { PartitionKey = "part1", RowKey = "1" },
                                                     new Product(207, "Shirt", "Clothes", 5.25) { PartitionKey = "part1", RowKey = "2" },
                                                     new Product(315, "Leather boots", "Shoes", 25.00)  { PartitionKey = "part1", RowKey = "3" },
                                                     new Product(108, "Necklace", "Accessories", 51.50)  { PartitionKey = "part1", RowKey = "4" },
                                                     new Product(202, "Shorts", "Clothes", 7.95)  { PartitionKey = "part1", RowKey = "5" } };


        private CloudTableClient CreateTableClient()
        {
            // Key angivet i kode - dårlig idé, når man benytter offentlig GitHub konto:
            //CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));

            // Key angivet i Azure application settings
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConfigurationManager.ConnectionStrings["StorageConnStr"].ConnectionString);
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            return tableClient;
        }

        internal void InitializeSampleData()
        {
            var ctcProduct = CreateTableClient();
            CloudTable productTblRef = ctcProduct.GetTableReference("Products3");

            if (productTblRef.CreateIfNotExists())
            {
                for (int i = 0; i < this.products.Length; i++)
                {
                    // Partition key kunne være Category - vil dog bevirke, at metoden GetProduct() ikke virker, 
                    // fordi den kun modtager Id-propertien
                    //this.products[i].PartitionKey = products[i].Category;

                    // Overskriv opr. rowkey med Id-propertien for at få GetProduct metoden til at virke
                    this.products[i].RowKey = products[i].Id.ToString();

                    TableOperation insertOperation = TableOperation.Insert(products[i]);
                    productTblRef.Execute(insertOperation);
                }
            }
        }

        public IEnumerable<Product> GetAllProducts()
        {
            var ctcProduct = CreateTableClient();
            CloudTable productTblRef = ctcProduct.GetTableReference("Products3");
            
            IEnumerable<Product> query =
                from product in productTblRef.CreateQuery<Product>()
                select product;

            // tvinge LINQ-forespørgsel til at blive kørt:
            return query.ToList();

            // Original løsning uden Azure storage:
            //return this.products;
        }

        public IHttpActionResult GetProduct(int id)
        {
            var ctcProduct = CreateTableClient();
            CloudTable productTblRef = ctcProduct.GetTableReference("Products3");

            IEnumerable<Product> query =
                from product in productTblRef.CreateQuery<Product>()
                where product.RowKey == id.ToString() && product.PartitionKey == "part1"
                select product;

            // tvinge LINQ-forespørgsel til at blive kørt:
            var findProduct = query.FirstOrDefault();

            if (findProduct != null)
            {
                return Ok(findProduct);
            }
            else
            {
                return NotFound();
            }

            // Original løsning uden Azure storage:
            //for (int i = 0; i < products.Length; i++)
            //{
            //    if (products[i].Id == id)
            //    {
            //        return Ok(products[i]);
            //    }
            //}
            //return NotFound();
        }
    }
}
