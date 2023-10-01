﻿using MeuPonto.Data;
using Microsoft.EntityFrameworkCore;

namespace MeuPonto.Infrastructure;

public static class DbModule
{
    public static IServiceCollection AddDbServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        services.AddDbContext<MeuPontoDbContext>(options =>
            options.UseSqlServer(connectionString));

        //

        return services;
    }

    public static void EnsureDatabaseExists(IServiceProvider serviceProvider)
    {
        using (var scope = serviceProvider.CreateScope())
        {
            var scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<MeuPontoDbContext>();
            var logger = scopedServices.GetRequiredService<ILogger<MeuPontoDbContext>>();

            logger.LogDebug("Starting database migration");

            try
            {
                db.Database.MigrateAsync();

                logger.LogDebug("Database migration finished");

                //Utilities.InitializeDbForTests(db);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred migrating the database" +
                    "Error: {Message}", ex.Message);
            }
        }
    }

    public static async Task EnsureDatabaseExistsAsync(IServiceProvider serviceProvider)
    {
        using (var scope = serviceProvider.CreateScope())
        {
            var scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<MeuPontoDbContext>();
            var logger = scopedServices.GetRequiredService<ILogger<MeuPontoDbContext>>();

            logger.LogDebug("Starting database migration");

            try
            {
                await db.Database.MigrateAsync();

                logger.LogDebug("Database migration finished");

                //Utilities.InitializeDbForTests(db);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred migrating the database" +
                    "Error: {Message}", ex.Message);
            }
        }
    }
}
