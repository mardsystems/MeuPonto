using MeuPonto.Modules.Empregadores;
using MeuPonto.Modules.Perfis;
using MeuPonto.Modules.Pontos;

namespace MeuPonto.Infrastructure;

public static class PresentationModule
{
    public static IServiceCollection AddWindows(this IServiceCollection services)
    {
        services.AddTransient(typeof(MainPage));

        //services.AddTransient(typeof(CadastroTrabalhadoresWindow));

        services.AddTransient(typeof(CadastroEmpregadoresPage));
        services.AddTransient(typeof(EmpregadorPage));

        services.AddTransient(typeof(CadastroPerfisPage));
        services.AddTransient(typeof(PerfilPage));

        services.AddTransient(typeof(RegistroPontosPage));

        //services.AddTransient(typeof(BackupComprovantesWindow));

        //services.AddTransient(typeof(GestaoFolhasWindow));

        return services;
    }
}
