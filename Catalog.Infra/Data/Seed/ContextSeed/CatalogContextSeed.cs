using Catalog.Core.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Catalog.Infra.Data.Seed.ContextSeed
{
    public class CatalogContextSeed
    {
        public static void SeedData(IMongoCollection<Product> productCollection)
        { 
            bool checkProducts = productCollection.Find(p => true).Any();
            string path = Path.Combine("Data", "SeedData", "products.json");
            if (!checkProducts)
            {
                var productsData = File.ReadAllText(path);
                var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                if (products != null)
                { 
                    foreach (var product in products)
                    {
                        productCollection.InsertOneAsync(product);
                    }
                }
            }
        
        }
    }
}
