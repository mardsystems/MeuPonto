using MeuPonto.Modules.Empregadores;
using MeuPonto.Modules.Perfis;
using MeuPonto.Modules.Pontos;
using System.Windows.Input;

namespace MeuPonto;

public partial class AppShell : Shell
{
    public ICommand HelpCommand { get; set; }

    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute("Empregadores/Empregador", typeof(EmpregadorPage));
        Routing.RegisterRoute("Perfis/Perfil", typeof(PerfilPage));
        Routing.RegisterRoute("Pontos/Ponto", typeof(PontoPage));
    }

    protected override void OnNavigating(ShellNavigatingEventArgs args)
    {
        base.OnNavigating(args);
    }
}
