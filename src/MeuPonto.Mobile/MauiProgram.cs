using CommunityToolkit.Maui;
using MeuPonto.Infrastructure;
using Microsoft.Extensions.Logging;

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

#if DEBUG
		builder.Logging.AddDebug();
#endif

        builder.Services.AddWindows();

        builder.Services.AddInfrastructure();

        var app = builder.Build();

        //using (var scope = app.Services.CreateScope())
        //{
        //    var db = scope.ServiceProvider.GetService<MeuPontoDbContext>();

        //    db.Database.EnsureDeleted();
        //    db.Database.Migrate();
        //}

        return app;
    }
}
