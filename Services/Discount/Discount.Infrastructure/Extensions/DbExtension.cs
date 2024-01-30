using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace Discount.Infrastructure.Extensions
{
    public static class DbExtension
    {
        public static IHost MigrateDatabase<TContext>(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var config = services.GetRequiredService<IConfiguration>();

                var logger = services.GetRequiredService<ILogger<TContext>>();

                try
                {
                    logger.LogInformation("Db migration started.");
                    ApplyMigration(config);
                    logger.LogInformation("Db migration completed.");
                }
                catch (Exception)
                {

                    throw;
                }

                return host;
            }
        }

        private static void ApplyMigration(IConfiguration config)
        {
            var connection = new NpgsqlConnection(config.GetValue<string>("DatabaseSettings: ConnectionString"));
            connection.Open();
            using var cmd = new NpgsqlCommand
            {
                Connection = connection
            };

            cmd.CommandText = "drop table if exists Coupon ";
            cmd.ExecuteNonQuery();
            cmd.CommandText = @"create table Coupon(Id SERIAL PRIMARY KEY ,
                                                  ProductName VARCHAR(500),
                                                  Description TEXT,
                                                  Amount INT)";
            cmd.ExecuteNonQueryAsync();
            cmd.CommandText = "insert into Coupon(ProductName,Description,Amount) values " +
                                                "('Adidas Quick Force Indoor Badminton Shoes','Shoe Discount',500)";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "insert into Coupon(ProductName,Description,Amount) values " +
                                                "('Yonex Super Ace Light Badminton Shoes','Racquet Discount',3500)";
            cmd.ExecuteNonQuery();

        }
    }

}

