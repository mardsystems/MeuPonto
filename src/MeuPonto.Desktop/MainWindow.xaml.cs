﻿using MeuPonto.Modules.Perfis;
using MeuPonto.Modules.Pontos;
using MeuPonto.Modules.Pontos.Comprovantes;
using MeuPonto.Modules.Pontos.Folhas;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;

namespace MeuPonto;

public partial class MainWindow : Window
{
    public IServiceProvider ServiceProvider { get; }

    public MainWindow(IServiceProvider serviceProvider)
    {
        InitializeComponent();

        ServiceProvider = serviceProvider;
    }

    private void CadastroPerfisMenuItem_Click(object sender, RoutedEventArgs e)
    {
        var cadastroPerfisWindow = ServiceProvider.GetRequiredService<CadastroPerfisWindow>();

        cadastroPerfisWindow.Show();
    }

    private void RegistroPontosMenuItem_Click(object sender, RoutedEventArgs e)
    {
        var registroPontosForm = ServiceProvider.GetRequiredService<RegistroPontosWindow>();

        registroPontosForm.Show();
    }

    private void GestaoFolhasMenuItem_Click(object sender, RoutedEventArgs e)
    {
        var gestaoFolhasForm = ServiceProvider.GetRequiredService<GestaoFolhasWindow>();

        gestaoFolhasForm.Show();
    }

    private void BackupComprovantesMenuItem_Click(object sender, RoutedEventArgs e)
    {
        var backupComprovantesForm = ServiceProvider.GetRequiredService<BackupComprovantesWindow>();

        backupComprovantesForm.Show();
    }

    private void configuracoesMenuItem_Click(object sender, RoutedEventArgs e)
    {
        //_db.Database.EnsureDeleted();

        //_db.Database.Migrate();

        //_db.Database.CloseConnection();

        //statusBarLabel.Content = "Sucesso.";
    }
}