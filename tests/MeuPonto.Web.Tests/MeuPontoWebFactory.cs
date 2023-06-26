using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Options;

namespace MeuPonto;

public class MeuPontoWebFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            services.AddAuthentication(defaultScheme: "TestScheme")
                .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>(
                    "TestScheme", options => { });

            //var dbContextDescriptor = services.SingleOrDefault(
            //    d => d.ServiceType ==
            //        typeof(DbContextOptions<MeuPontoDbContext>));

            //services.Remove(dbContextDescriptor);

            ////var dbConnectionDescriptor = services.SingleOrDefault(
            ////    d => d.ServiceType ==
            ////        typeof(DbConnection));

            ////services.Remove(dbConnectionDescriptor);

            ////// Create open SqliteConnection so EF won't automatically close it.
            ////services.AddSingleton<DbConnection>(container =>
            ////{
            ////    var connection = new SqliteConnection("DataSource=:memory:");
            ////    connection.Open();

            ////    return connection;
            ////});

            //services.AddDbContext<MeuPontoDbContext>((container, options) =>
            //{
            //    //var connection = container.GetRequiredService<DbConnection>();
            //    //options.UseSqlite(connection);
            //    options.UseInMemoryDatabase("InMemoryDbForTesting");
            //});
        });

        builder.UseEnvironment("Development");
    }
}

public class TestAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    public TestAuthHandler(IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
        : base(options, logger, encoder, clock)
    {
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var claims = new[] { new Claim(ClaimTypes.Name, "Test user") };
        var identity = new ClaimsIdentity(claims, "Test");
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, "TestScheme");

        var result = AuthenticateResult.Success(ticket);

        return Task.FromResult(result);
    }
}
