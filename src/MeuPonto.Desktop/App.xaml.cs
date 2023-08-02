using MeuPonto.Data;
using MeuPonto.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows;

namespace MeuPonto;

public partial class App : Application
{
    public IServiceProvider ServiceProvider { get; private set; }

    public IConfiguration Configuration { get; private set; }

    public App()
    {
        var culture = CultureInfo.CreateSpecificCulture("pt-BR");

        Thread.CurrentThread.CurrentCulture = culture;

        Thread.CurrentThread.CurrentUICulture = culture;

        CultureInfo.DefaultThreadCurrentUICulture = culture;
    }

    protected override async void OnStartup(StartupEventArgs e)
    {
        var basePath = Directory.GetCurrentDirectory();

        var builder = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddUserSecrets(Assembly.GetExecutingAssembly())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false);

        Configuration = builder.Build();

        var serviceCollection = new ServiceCollection();

        {
            var endpointUri = Configuration.GetConnectionString("EndpointUri") ?? throw new InvalidOperationException("EndpointUri not found.");
            var primaryKey = Configuration.GetConnectionString("PrimaryKey") ?? throw new InvalidOperationException("PrimaryKey not found.");

            serviceCollection.AddDbContext<MeuPontoDbContext>(options =>
                options.UseCosmos(endpointUri, primaryKey, databaseName: "MeuPonto"));
        }

        ConfigureServices(serviceCollection);

        ServiceProvider = serviceCollection.BuildServiceProvider(validateScopes: true);

        var serviceScopeFactory = ServiceProvider.GetRequiredService<IServiceScopeFactory>();

        //await DbModule.EnsureDatabaseCreatedAsync(serviceScopeFactory);

        var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();

        mainWindow.Show();
    }

    private void ConfigureServices(IServiceCollection services)
    {
        services.AddWindows();

        services.AddInfrastructure();
    }
}
