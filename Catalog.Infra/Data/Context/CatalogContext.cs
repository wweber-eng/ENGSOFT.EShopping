using Catalog.Core.Entities;
using Catalog.Infra.Data.Repository;
using Catalog.Infra.Data.Seed.ContextSeed;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Catalog.Infra.Data.Context
{
    public class CatalogContext : ICatalogContext
    {
        public IMongoCollection<Product> Products { get; }
        public IMongoCollection<ProductBrand> Brands { get; }
        public IMongoCollection<ProductType> Types { get; }

        public CatalogContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<string>(key:"DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(name: configuration.GetValue<string>("DatabaseSettings:DatabaseName"));
            Brands = database.GetCollection<ProductBrand>(
                name: configuration.GetValue<string>(key: "DatabaseSettings:BrandsCollection"));
            Types = database.GetCollection<ProductType>(
                name: configuration.GetValue<string>(key: "DatabaseSettings:TypesCollection"));
            Products = database.GetCollection<Product>(
                name: configuration.GetValue<string>(key: "DatabaseSettings:CollectionName"));

            BrandContextSeed.SeedData(Brands);
            TypeContextSeed.SeedData(Types);
            CatalogContextSeed.SeedData(Products);
        }
    }
}
