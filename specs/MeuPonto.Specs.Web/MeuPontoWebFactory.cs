using MeuPonto.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace MeuPonto;

public class MeuPontoWebFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            //var dbContextDescriptor = services.SingleOrDefault(d =>
            //    d.ServiceType == typeof(DbContextOptions<MeuPontoDbContext>));

            //services.Remove(dbContextDescriptor);

            //var dbConnectionDescriptor = services.SingleOrDefault(d =>
            //    d.ServiceType == typeof(DbConnection));

            //services.Remove(dbConnectionDescriptor);

            //// Create open SqliteConnection so EF won't automatically close it.
            //services.AddSingleton<DbConnection>(container =>
            //{
            //    var connection = new SqliteConnection("DataSource=:memory:");
            //    connection.Open();

            //    return connection;
            //});

            //services.AddDbContext<MeuPontoDbContext>((container, options) =>
            //{
            //    var connection = container.GetRequiredService<DbConnection>();
            //    options.UseSqlite(connection);

            //    //options.UseInMemoryDatabase("InMemoryDbForTesting");
            //});

            services.AddAuthentication(defaultScheme: "TestScheme")
                .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>(
                    "TestScheme", options => { });

            //var sp = services.BuildServiceProvider();

            //using (var scope = sp.CreateScope())
            //{
            //    var scopedServices = scope.ServiceProvider;
            //    var db = scopedServices.GetRequiredService<MeuPontoDbContext>();
            //    var logger = scopedServices
            //        .GetRequiredService<ILogger<MeuPontoDbContext>>();

            //    logger.LogDebug("EnsureDeleted");

            //    db.Database.EnsureDeleted();

            //    logger.LogDebug("EnsureCreated");

            //    db.Database.EnsureCreated();

            //    try
            //    {
            //        //Utilities.InitializeDbForTests(db);
            //    }
            //    catch (Exception ex)
            //    {
            //        logger.LogError(ex, "An error occurred seeding the " +
            //            "database with test messages. Error: {Message}", ex.Message);
            //    }
            //}
        });

        builder.UseEnvironment("Development");
    }
}
