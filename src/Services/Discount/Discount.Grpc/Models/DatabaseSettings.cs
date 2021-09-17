using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discount.Grpc.Models
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string ConnectionString { get; set; }
    }

    public interface IDatabaseSettings
    {
        string ConnectionString { get; set; }
    }
}
