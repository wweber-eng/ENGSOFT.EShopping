﻿using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using Catalog.Infra.Data.Repository;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Infra.Repositories
{
    public class ProductRepository : IProductRepository, IBrandRepository, ITypesRepository
    {
        private readonly ICatalogContext _context;

        public ProductRepository(ICatalogContext context)
        {
            _context = context;

        }

        #region Produtos

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _context
                .Products
                .Find(filter: p => true)
                .ToListAsync();
        }

        public async Task<Product> GetProduct(string id)
        {
            return await _context
                .Products
                .Find(filter: p => p.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByName(string name)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(field: p => p.Name, name);

            return await _context
                .Products
                .Find(filter)
                .ToListAsync();
        }

        public async Task<Product> CreateProduct(Product product)
        {
            await _context
                .Products
                .InsertOneAsync(product);

            return product;
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            var updateResult =  await _context
                .Products.ReplaceOneAsync(
                    p => p.Id == product.Id, product);

            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }

        public async Task<bool> DeleteProduct(string id)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(field: p => p.Id, id);
            DeleteResult deleteResult = await _context
                .Products
                .DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;

        }

        public async Task<IEnumerable<Product>> GetProductByBrand(string name)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(field: p => p.Brands.Name, name);
            return await _context
                .Products
                .Find(filter)
                .ToListAsync();
        }

        #endregion

        public async Task<IEnumerable<ProductBrand>> GetAllBrands()
        {
            return await _context
                .Brands
                .Find(filter: p => true)
                .ToListAsync();
        }

        public async Task<IEnumerable<ProductType>> GetAllTypes()
        {
            return await _context
                .Types
                .Find(filter: p => true)
                .ToListAsync();
        }

    }
}
