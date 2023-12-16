using MeuPonto.Pages.Empregadores;
using MeuPonto.Pages.Perfis;
using MeuPonto.Pages.Pontos;
using MeuPonto.Pages.Pontos.Comprovantes;
using MeuPonto.Pages.Pontos.Folhas;
using MeuPonto.Pages.Trabalhadores;
using Microsoft.Extensions.DependencyInjection;

namespace MeuPonto.Infrastructure;

public static class PresentationModule
{
    public static IServiceCollection AddWindows(this IServiceCollection services)
    {
        services.AddTransient(typeof(MainWindow));

        services.AddTransient(typeof(CadastroTrabalhadoresWindow));

        services.AddTransient(typeof(CadastroEmpregadoresWindow));

        services.AddTransient(typeof(CadastroPerfisWindow));

        services.AddTransient(typeof(RegistroPontosWindow));

        services.AddTransient(typeof(BackupComprovantesWindow));

        services.AddTransient(typeof(GestaoFolhasWindow));

        return services;
    }
}
