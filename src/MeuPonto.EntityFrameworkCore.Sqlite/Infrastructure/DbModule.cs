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
        var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        var dataSource = Path.Combine(appDataPath, "MeuPonto.db");

        services.AddDbContext<MeuPontoDbContext>(options =>
            options.UseSqlite($"Data Source={dataSource}", b => b.MigrationsAssembly("MeuPonto.EntityFrameworkCore.Sqlite")));

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
