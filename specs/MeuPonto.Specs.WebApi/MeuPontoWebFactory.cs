using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Diagnostics;

namespace MeuPonto;

public class MeuPontoWebFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        base.ConfigureWebHost(builder);
        builder.ConfigureLogging((context, loggingBuilder) =>
        {
            loggingBuilder.ClearProviders();
            loggingBuilder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<ILoggerProvider, DebugLoggerProvider>());
        });

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

            //services.AddAuthentication(defaultScheme: "TestScheme")
            //    .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>(
            //        "TestScheme", options => { });

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
    
    #region Debug Logger
    [ProviderAlias("Debug")]
    // ReSharper disable once ClassNeverInstantiated.Local
    class DebugLoggerProvider : ILoggerProvider
    {
        public ILogger CreateLogger(string name) => new DebugLogger(name);

        public void Dispose()
        {
        }
    }

    class DebugLogger : ILogger
    {
        private readonly string _name;

        public DebugLogger(string name)
        {
            _name = string.IsNullOrEmpty(name) ? nameof(DebugLogger) : name;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return NoopDisposable.Instance;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return Debugger.IsAttached && logLevel != LogLevel.None;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
                return;

            var message = formatter(state, exception);
            if (string.IsNullOrEmpty(message))
                return;

            message = $"{logLevel}: {message}";

            if (exception != null)
                message += Environment.NewLine + Environment.NewLine + exception;

            Debug.WriteLine(message, _name);
        }

        private class NoopDisposable : IDisposable
        {
            public static readonly NoopDisposable Instance = new NoopDisposable();

            public void Dispose()
            {
            }
        }
    }
    #endregion
}
