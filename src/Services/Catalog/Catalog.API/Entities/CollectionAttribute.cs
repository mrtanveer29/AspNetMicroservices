using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Entities
{
    [AttributeUsage(AttributeTargets.Class,Inherited =false)]
    public class CollectionAttribute :Attribute
    {
        public string CollectionName { get;  }

        public CollectionAttribute(string collectionName)
        {
            CollectionName = collectionName;
        }

       
    }
}
