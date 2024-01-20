using Billing.Models;
using MeuPonto.Authentication;
using MeuPonto.Cache;
using MeuPonto.Data;
using MeuPonto.Infrastructure;
using MeuPonto.Models.Trabalhadores;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using System.Security.Claims;
using System.Transactions;

namespace MeuPonto;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddInfrastructure(builder.Configuration);

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

                var userId = nameIdentifier.Value;

                var trabalhadorExistente = await db.Trabalhadores.FirstOrDefaultAsync(m => m.UserId == userId);

                var userName = context.Principal.GetDisplayName();

                Trabalhador trabalhador;

                var transaction = new TransactionContext(userId.ToString());

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

                policy.RequireClaim("SubscriptionPlanId", SubscriptionPlanEnum.Silver.ToString(), SubscriptionPlanEnum.Gold.ToString());
            });
        });

        builder.Services
            .AddRazorPages(options =>
            {
                //options.RootDirectory = "/Modules";
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
        //app.MapFallbackToFile("app/index.html");

        DbModule.EnsureDatabaseExists(app.Services);

        app.Run();
    }
}