using MeuPonto.Modules.Empregadores;
using MeuPonto.Modules.Perfis;
using MeuPonto.Modules.Pontos;
using MeuPonto.Modules.Pontos.Comprovantes;
using MeuPonto.Modules.Pontos.Folhas;
using MeuPonto.Modules.Trabalhadores;
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
