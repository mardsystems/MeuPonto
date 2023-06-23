using MeuPonto.Modules.Perfis;
using MeuPonto.Modules.Pontos;
using MeuPonto.Modules.Pontos.Folhas;
using Microsoft.Extensions.DependencyInjection;

namespace MeuPonto.Infrastructure;

public static class PresentationModule
{
    public static IServiceCollection AddWindows(this IServiceCollection services)
    {
        services.AddTransient(typeof(MainWindow));

        services.AddTransient(typeof(CadastroPerfisWindow));

        services.AddTransient(typeof(RegistroPontosWindow));

        services.AddTransient(typeof(GestaoFolhasWindow));

        return services;
    }
}
