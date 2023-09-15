using MeuPonto.Cache;
using MeuPonto.Data;
using MeuPonto.Modules.Trabalhadores;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using System.Security.Claims;

namespace MeuPonto;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

#if INFRA_COSMOS
        {
            var endpointUri = builder.Configuration.GetConnectionString("EndpointUri") ?? throw new InvalidOperationException("EndpointUri not found.");
            var primaryKey = builder.Configuration.GetConnectionString("PrimaryKey") ?? throw new InvalidOperationException("PrimaryKey not found.");

            builder.Services.AddDbContext<MeuPontoDbContext>(options =>
                options.UseCosmos(endpointUri, primaryKey, databaseName: "MeuPonto"));
        }
#endif

#if INFRA_SQLITE
        {
            var basePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            var dataSource = Path.Combine(basePath, "MeuPonto.db");

            builder.Services.AddDbContext<MeuPontoDbContext>(options =>
                options.UseSqlite($"Data Source={dataSource}", b => b.MigrationsAssembly("MeuPonto.EntityFrameworkCore.Sqlite")));
        }
#endif

#if INFRA_SQLSERVER
        {
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            builder.Services.AddDbContext<MeuPontoDbContext>(options =>
                options.UseSqlServer(connectionString, b => b.MigrationsAssembly("MeuPonto.EntityFrameworkCore.SqlServer")));
        }
#endif

        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
            .AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAd"));

        builder.Services.Configure<OpenIdConnectOptions>(OpenIdConnectDefaults.AuthenticationScheme, options =>
        {
            var previousOptions = options.Events.OnRedirectToIdentityProvider;
            options.Events.OnRedirectToIdentityProvider = async context =>
            {
                await previousOptions(context);

                //https://github.com/Azure-Samples/active-directory-aspnetcore-webapp-openidconnect-v2/issues/399#issuecomment-681917473
                context.ProtocolMessage.ResponseType = Microsoft.IdentityModel.Protocols.OpenIdConnect.OpenIdConnectResponseType.IdToken;
            };

            var onTicketReceived = options.Events.OnTicketReceived;

            options.Events.OnTicketReceived = async context =>
            {
                onTicketReceived?.Invoke(context);

                var db = context.HttpContext.RequestServices.GetService<MeuPontoDbContext>();

                var nameIdentifier = context.Principal.FindFirst(ClaimTypes.NameIdentifier);

                var userId = Guid.Parse(nameIdentifier.Value);

                var trabalhadorExistente = await db.Trabalhadores.FirstOrDefaultAsync(m => m.Id == userId);

                var userName = context.Principal.GetDisplayName();

                Trabalhador trabalhador;

                var transaction = new TransactionContext(userId);

                if (trabalhadorExistente == default)
                {
                    trabalhador = TrabalhadorFactory.CriaTrabalhador(transaction);

                    try
                    {
                        db.Trabalhadores.Add(trabalhador);
                        await db.SaveChangesAsync();
                    }
                    catch (Exception _)
                    {
                        throw;
                    }
                }
                else
                {
                    trabalhador = trabalhadorExistente;
                }

                //Trabalhador.Default = trabalhador;
            };

            options.Events.OnRedirectToIdentityProviderForSignOut = async context =>
            {
                //Trabalhador.Default = null;
            };
        });

        builder.Services.AddAuthorization(options =>
        {
            // By default, all incoming requests will be authorized according to the default policy.
            options.FallbackPolicy = options.DefaultPolicy;

            var rolesSection = builder.Configuration.GetSection("Roles");

            var userAdmin = rolesSection.GetValue<string>("Admin");

            options.AddPolicy("Admin", policy => policy.RequireClaim(ClaimTypes.NameIdentifier, userAdmin));

            options.AddPolicy("NoAds", policy =>
            {
                policy.RequireAuthenticatedUser();

                policy.RequireClaim("SubscriptionPlanId", Billing.SubscriptionPlanEnum.Silver.ToString(), Billing.SubscriptionPlanEnum.Gold.ToString());
            });
        });
        builder.Services
            .AddRazorPages(options =>
            {
                options.RootDirectory = "/Modules";
            })
            .AddMvcOptions(options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                              .RequireAuthenticatedUser()
                              .Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            })
            .AddViewOptions(options =>
            {
                options.HtmlHelperOptions.ClientValidationEnabled = false;
            })
            .AddMicrosoftIdentityUI();

        builder.Services.AddTransient(p => new DateTimeSnapshot(DateTime.Now));

        builder.Services.AddTransient<IClaimsTransformation, MyClaimsTransformation>();

        var app = builder.Build();

        app.UseMiddleware<CacheMiddleware>();

        var supportedCultures = new[] { "pt-BR", "en-US" };
        var localizationOptions = new RequestLocalizationOptions().SetDefaultCulture(supportedCultures[0])
            .AddSupportedCultures(supportedCultures)
            .AddSupportedUICultures(supportedCultures);

        app.UseRequestLocalization(localizationOptions);

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();

            app.UseWebAssemblyDebugging();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();


        app.UseBlazorFrameworkFiles();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapRazorPages();
        app.MapControllers();
        app.MapFallbackToFile("app/index.html");

#if INFRA_SQLITE || INFRA_SQLSERVER
        using (var scope = app.Services.CreateScope())
        {
            var scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<MeuPontoDbContext>();
            var logger = scopedServices
                .GetRequiredService<ILogger<MeuPontoDbContext>>();

            logger.LogDebug("Starting database migration");

            db.Database.MigrateAsync();

            logger.LogDebug("Database migration finished");

            try
            {
                //Utilities.InitializeDbForTests(db);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred seeding the " +
                    "database with test messages. Error: {Message}", ex.Message);
            }
        }
#endif

        app.Run();
    }
}