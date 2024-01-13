using MeuPonto.Pages.Empregadores;
using MeuPonto.Pages.Contratos;
using MeuPonto.Pages.Pontos;
using MeuPonto.Pages.Pontos.Comprovantes;

namespace MeuPonto.Infrastructure;

public static class PresentationModule
{
    public static IServiceCollection AddWindows(this IServiceCollection services)
    {
        services.AddTransient(typeof(MainPage));

        //services.AddTransient(typeof(CadastroTrabalhadoresWindow));

        services.AddTransient(typeof(CadastroEmpregadoresPage));
        services.AddTransient(typeof(EmpregadorPage));

        services.AddTransient(typeof(GestaoContratosPage));
        services.AddTransient(typeof(ContratoPage));

        services.AddTransient(typeof(RegistroPontosPage));
        services.AddTransient(typeof(PontoPage));

        services.AddTransient(typeof(BackupComprovantesPage));
        services.AddTransient(typeof(GuardarComprovantePage));

        //services.AddTransient(typeof(GestaoFolhasWindow));

        return services;
    }
}
