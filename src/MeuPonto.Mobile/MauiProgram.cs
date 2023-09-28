using CommunityToolkit.Maui;
using MeuPonto.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace MeuPonto;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        builder.Configuration
            .AddUserSecrets(Assembly.GetExecutingAssembly());

#if DEBUG
        builder.Logging.AddDebug();
#endif

        builder.Services.AddWindows();

        builder.Services.AddInfrastructure(builder.Configuration);

        var app = builder.Build();

        DbModule.EnsureDatabaseExists(app.Services);

        return app;
    }
}
