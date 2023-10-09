﻿using Npgsql;

namespace Discount.API.Extensions
{
    public static class ServicesExtensions
    {
        public static WebApplication MigrateDatabase<T>(this WebApplication app, int? retry = 0)
        {
            int retryForAvailability = retry.Value;

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var configuration = services.GetService<IConfiguration>();
                var logger = services.GetService<ILogger>();

                try
                {
                    logger?.LogInformation("Migrating postgresql database");

                    using var connection = new NpgsqlConnection(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
                    connection.Open();

                    using var command = new NpgsqlCommand { Connection = connection };

                    command.CommandText = "DROP TABLE IF EXISTS Coupon";
                    command.ExecuteNonQuery();

                    command.CommandText = @"CREATE TABLE Coupon(
                                                    ID SERIAL PRIMARY KEY NOT NULL,
                                                    ProductName VARCHAR(24) NOT NULL,
                                                    Description TEXT,
                                                    Amount INT);";
                    command.ExecuteNonQuery();

                    command.CommandText = "INSERT INTO Coupon (ProductName, Description, Amount) VALUES('IPhone X', 'IPhone Discount', 150);";
                    command.ExecuteNonQuery();

                    command.CommandText = "INSERT INTO Coupon (ProductName, Description, Amount) VALUES('Samsung 10', 'Samsung Discount', 100);";
                    command.ExecuteNonQuery();

                    logger?.LogInformation("Migrated postgresql database");
                }
                catch (NpgsqlException ex)
                {
                    logger?.LogError(ex, "An error occured while migrating the postgresql database");

                    if (retryForAvailability < 50)
                    {
                        retryForAvailability++;
                        Thread.Sleep(2000);
                        MigrateDatabase<T>(app, retryForAvailability);
                    }
                }
                return app;
            }
        }
    }
}
