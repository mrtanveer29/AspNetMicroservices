using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Entities
{
    public class Document : IDocument
    {
        public string Id { get; set; }

        public DateTime CreatedAt => DateTime.Now;
    }
}
