
using Discount.Grpc.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discount.Grpc.Data
{
    public class DapperDbContext
    {
        private IDatabaseSettings _databaseSettings;
        public DapperDbContext(IDatabaseSettings settings)
        {
            this._databaseSettings = settings;
        }

        public NpgsqlConnection Getconnection()
        {
            return new NpgsqlConnection(_databaseSettings.ConnectionString);
        }
    }
}
