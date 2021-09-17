using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Models
{
    public class CatalogDatabaseSettings : ICatalogDatabaseSettings
    {
        public string ProductsCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface ICatalogDatabaseSettings
    {
        string ProductsCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
