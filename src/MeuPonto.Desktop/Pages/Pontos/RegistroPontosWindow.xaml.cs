using MeuPonto.Models.Timesheet.Pontos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace MeuPonto.Pages.Pontos;

public partial class RegistroPontosWindow : Window
{
    private readonly IServiceScope _scope;

    private readonly Data.MeuPontoDbContext _db;

    private CollectionViewSource _pontosViewSource;

    private CollectionViewSource _perfisViewSource;

    private ObservableCollection<Ponto> _pontos;

    public RegistroPontosWindow(IServiceProvider serviceProvider)
    {
        InitializeComponent();

        _scope = serviceProvider.CreateScope();

        _db = _scope.ServiceProvider.GetRequiredService<Data.MeuPontoDbContext>();
    }

    private async void Window_Loaded(object sender, RoutedEventArgs e)
    {
        Cursor = Cursors.Wait;

        SetStatusBar("Carregando registro de pontos...");

        _pontosViewSource = ((CollectionViewSource)(this.FindResource("pontosViewSource")));

        await _db.Pontos
            //.Include(p => p.Recursos)
            .LoadAsync();

        _pontos = _db.Pontos.Local.ToObservableCollection();

        foreach (var ponto in _pontos)
        {
            //modelo.RecursosChanged += Modelo_RecursosChanged;
        }

        _pontosViewSource.Source = _pontos;

        Cursor = null;

        //

        SetStatusBar("Carregando perfis...");

        _perfisViewSource = ((CollectionViewSource)(this.FindResource("perfisViewSource")));

        var perfis = await _db.Perfis
            .ToListAsync();

        _perfisViewSource.Source = perfis;

        SetStatusBar("Pronto.");
    }

    private void SetStatusBar(string value)
    {
        statusBarLabel.Content = value;

        //statusBarTimer.Enabled = true;
    }

    private async void refreshButton_Click(object sender, RoutedEventArgs e)
    {
        Cursor = Cursors.Wait;

        SetStatusBar("Carregando registro de pontos...");

        await _db.Pontos
            //.Include(p => p.Recursos)
            .LoadAsync();

        _pontos = _db.Pontos.Local.ToObservableCollection();

        _pontosViewSource.Source = _pontos;

        SetStatusBar("Pronto.");

        Cursor = null;
    }

    private async void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        //CollectionViewSource modelosViewSource = ((CollectionViewSource)(this.FindResource("modelosViewSource")));

        //var observableCollection = (ModelosCollection)modelosViewSource.Source;

        try
        {
            await _db.SaveChangesAsync();

            SetStatusBar("Pontos salvos com sucesso.");
        }
        catch (Exception ex)
        {
            SetStatusBar(ex.Message);
        }
    }

    private void Window_Unloaded(object sender, RoutedEventArgs e)
    {
        //_db.Database.CloseConnection();

        _db.Dispose();

        _scope.Dispose();
    }
}
