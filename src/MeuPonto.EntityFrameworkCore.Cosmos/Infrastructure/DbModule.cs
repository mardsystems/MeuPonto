using MeuPonto.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MeuPonto.Infrastructure;

public static class DbModule
{
    public static IServiceCollection AddDbServices(this IServiceCollection services, IConfiguration configuration)
    {
        var endpointUri = configuration.GetConnectionString("EndpointUri") ?? throw new InvalidOperationException("EndpointUri not found.");
        var primaryKey = configuration.GetConnectionString("PrimaryKey") ?? throw new InvalidOperationException("PrimaryKey not found.");

        services.AddDbContext<MeuPontoDbContext>(options =>
            options.UseCosmos(endpointUri, primaryKey, databaseName: "MeuPonto"));

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

            logger.LogDebug("Starting database creation");

            try
            {
                db.Database.EnsureCreated();

                logger.LogDebug("Database creation finished");

                //Utilities.InitializeDbForTests(db);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred creating the database" +
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

            logger.LogDebug("Starting database creation");

            try
            {
                await db.Database.EnsureCreatedAsync();

                logger.LogDebug("Database creation finished");

                //Utilities.InitializeDbForTests(db);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred creating the database" +
                    "Error: {Message}", ex.Message);
            }
        }
    }
}
