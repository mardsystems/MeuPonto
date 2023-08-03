using MeuPonto.Data;
using Microsoft.EntityFrameworkCore;

namespace MeuPonto.Infrastructure;

public static class DbModule
{
    internal static IServiceCollection AddDbServices(this IServiceCollection services)
    {
        var basePath = FileSystem.AppDataDirectory;

        var dataSource = Path.Combine(basePath, "MeuPonto.db");

        services.AddDbContext<MeuPontoDbContext>(options =>
            options.UseSqlite($"Data Source={dataSource}", b => b.MigrationsAssembly("MeuPonto.EntityFrameworkCore.Sqlite")));

        //

        //ServiceProvider = services.BuildServiceProvider();

        //

        //var connectionString = ConfigurationManager.ConnectionStrings["Atelie"].ToString();

        //var builder = new DbContextOptionsBuilder<AtelieDbContext>();

        //container.Register(() => new AtelieDbContext(builder.UseSqlite(connectionString).Options), Lifestyle.Singleton);

        //container.Register<MeuPontoDbContext>(Lifestyle.Singleton);

        //

        return services;
    }

    public static void EnsureDatabaseCreated(IServiceScopeFactory serviceScopeFactory)
    {
        using (var serviceScope = serviceScopeFactory.CreateScope())
        {
            var db = serviceScope.ServiceProvider.GetService<MeuPontoDbContext>();

            db.Database.EnsureCreated();

            //await db.Database.MigrateAsync();
        }
    }

    public static async Task EnsureDatabaseCreatedAsync(IServiceScopeFactory serviceScopeFactory)
    {
        using (var serviceScope = serviceScopeFactory.CreateScope())
        {
            var db = serviceScope.ServiceProvider.GetService<MeuPontoDbContext>();

            //await db.Database.EnsureCreatedAsync();

            //await db.Database.MigrateAsync();
        }
    }
}
