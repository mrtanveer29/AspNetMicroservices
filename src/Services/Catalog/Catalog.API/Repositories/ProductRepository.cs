using Catalog.API.Data;
using Catalog.API.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private MongoDbContext _context;
        private IMongoCollection<Product> Products;
        public ProductRepository(MongoDbContext context) 
        {
            _context = context;
            Products = context.GetCollection<Product>();
        }
        public async Task<IEnumerable<Product>> GetProducts() {
            return await Products.Find(x => true).ToListAsync();
        }
        public async Task<Product> GetProduct(string id)
        {
            return await Products
                           .Find(p => p.Id == id)
                           .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByName(string name)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Name, name);

            return await Products
                            .Find(filter)
                            .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByCategory(string categoryName)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Category, categoryName);

            return await Products
                            .Find(filter)
                            .ToListAsync();
        }

        public async Task CreateProduct(Product product)
        {
            await Products.InsertOneAsync(product);
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            var updateResult = await Products
                                        .ReplaceOneAsync(filter: g => g.Id == product.Id, replacement: product);

            return updateResult.IsAcknowledged
                    && updateResult.ModifiedCount > 0;
        }

        public async Task<bool> DeleteProduct(string id)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Id, id);

            DeleteResult deleteResult = await Products
                                                .DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged
                && deleteResult.DeletedCount > 0;
        }

    }

}