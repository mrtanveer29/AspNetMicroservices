
using Discount.Grpc.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Discount.Grpc.Extentions
{
    public static class HostExtensions
    {
        public static IHost MigrateDatabse<TContext>(this IHost host, int? retry= 0)
        {
            int retryForAvailability = retry.Value;

            using (var scope=host.Services.CreateScope()) {
              IServiceProvider  sp=scope.ServiceProvider;
                var Dbcontext = sp.GetRequiredService<DapperDbContext>();
                var logger = sp.GetRequiredService<ILogger<TContext>>();
                try
                {
                    logger.LogInformation("Migration Started");
                   using var con = Dbcontext.Getconnection();
                    con.Open();

                    using var command = new NpgsqlCommand
                    {
                        Connection = con
                    };

                    command.CommandText = "DROP TABLE IF EXISTS Coupon";
                    command.ExecuteNonQuery();

                    command.CommandText = @"CREATE TABLE Coupon(Id SERIAL PRIMARY KEY, 
                                                                ProductName VARCHAR(24) NOT NULL,
                                                                Description TEXT,
                                                                Amount INT)";
                    command.ExecuteNonQuery();

                    command.CommandText = "INSERT INTO Coupon(ProductName, Description, Amount) VALUES('IPhone X', 'IPhone Discount', 150);";
                    command.ExecuteNonQuery();

                    command.CommandText = "INSERT INTO Coupon(ProductName, Description, Amount) VALUES('Samsung 10', 'Samsung Discount', 100);";
                    command.ExecuteNonQuery();

                    logger.LogInformation("Migrated postresql database.");


                }
                catch (NpgsqlException ex)
                {
                    logger.LogError(ex.Message);
                    if (retryForAvailability < 50)
                    {
                        retryForAvailability++;
                        Thread.Sleep(2000);
                        MigrateDatabse<TContext>(host, retryForAvailability);
                    }
                }
                
            }
            return host;
        }
    }
}
