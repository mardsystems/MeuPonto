using MeuPonto.Cache;
using MeuPonto.Data;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;

namespace MeuPonto;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        var endpointUri = builder.Configuration.GetConnectionString("EndpointUri") ?? throw new InvalidOperationException("EndpointUri not found.");
        var primaryKey = builder.Configuration.GetConnectionString("PrimaryKey") ?? throw new InvalidOperationException("PrimaryKey not found.");
        builder.Services.AddDbContext<MeuPontoDbContext>(options =>
            options.UseCosmos(endpointUri, primaryKey, databaseName: "MeuPonto"));
        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
            .AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAd"));

        builder.Services.AddAuthorization(options =>
        {
            // By default, all incoming requests will be authorized according to the default policy.
            options.FallbackPolicy = options.DefaultPolicy;
        });
        builder.Services
            .AddRazorPages(options =>
            {
                options.RootDirectory = "/Modules";
            })
            .AddViewOptions(options =>
            {
                options.HtmlHelperOptions.ClientValidationEnabled = false;
            })
            .AddMicrosoftIdentityUI();

        builder.Services.AddTransient(p => new DateTimeSnapshot(DateTime.Now));

        var app = builder.Build();

        var supportedCultures = new[] { "pt-BR", "en-US" };
        var localizationOptions = new RequestLocalizationOptions().SetDefaultCulture(supportedCultures[0])
            .AddSupportedCultures(supportedCultures)
            .AddSupportedUICultures(supportedCultures);

        app.UseRequestLocalization(localizationOptions);

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapRazorPages();
        app.MapControllers();

        app.UseMiddleware<CacheMiddleware>();

        //using (var scope = app.Services.CreateScope())
        //{
        //    var db = scope.ServiceProvider.GetService<MeuPontoDbContext>();

        //    db.Database.EnsureDeleted();
        //    db.Database.EnsureCreated();
        //}

        app.Run();
    }
}