﻿using MeuPonto.Pages.Empregadores;
using MeuPonto.Pages.Contratos;
using MeuPonto.Pages.Pontos;
using MeuPonto.Pages.Pontos.Comprovantes;
using System.Windows.Input;

namespace MeuPonto;

public partial class AppShell : Shell
{
    public ICommand HelpCommand { get; set; }

    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute("Empregadores/Empregador", typeof(EmpregadorPage));
        Routing.RegisterRoute("Contratos/Contrato", typeof(ContratoPage));
        Routing.RegisterRoute("Pontos/Ponto", typeof(PontoPage));
        Routing.RegisterRoute("Pontos/Comprovantes", typeof(BackupComprovantesPage));
        Routing.RegisterRoute("Pontos/Comprovantes/GuardarComprovante", typeof(GuardarComprovantePage));
    }

    protected override void OnNavigating(ShellNavigatingEventArgs args)
    {
        base.OnNavigating(args);
    }
}
