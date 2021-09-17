using Catalog.API.Entities;
using Catalog.API.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Data
{
    public class MongoDbContext
    {
        private MongoClient _client;
        private IMongoDatabase _db;
        public MongoDbContext(ICatalogDatabaseSettings settings)
        {
            _client = new MongoClient(settings.ConnectionString);
            _db = _client.GetDatabase(settings.DatabaseName);

            CatalogContextSeeder.SeedData(GetCollection<Product>());
        }
        public IMongoCollection<T>  GetCollection<T> () where T : IDocument
        {
            var type = typeof(T);
            var collectionName=((CollectionAttribute)type.GetCustomAttributes(typeof(CollectionAttribute), true).FirstOrDefault()).CollectionName;
            return _db.GetCollection<T>(collectionName ?? type.Name);
        }
        
    }
}
