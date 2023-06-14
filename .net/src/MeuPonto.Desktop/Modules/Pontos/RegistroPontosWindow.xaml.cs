using Microsoft.EntityFrameworkCore;
using System;
using System.Windows;
using System.Windows.Data;

namespace MeuPonto.Modules.Pontos;

public partial class RegistroPontosWindow : Window
{
    private readonly Data.MeuPontoDbContext _db;

    public RegistroPontosWindow(Data.MeuPontoDbContext db)
    {
        InitializeComponent();
        
        _db = db;
    }

    private async void Window_Loaded(object sender, RoutedEventArgs e)
    {
        CollectionViewSource pontosViewSource = ((CollectionViewSource)(this.FindResource("pontosViewSource")));

        await _db.Pontos
            //.Include(p => p.Recursos)
            .LoadAsync();

        var pontos = _db.Pontos.Local.ToObservableCollection();

        foreach (var ponto in pontos)
        {
            //modelo.RecursosChanged += Modelo_RecursosChanged;
        }

        pontosViewSource.Source = pontos;

        //

        CollectionViewSource perfisViewSource = ((CollectionViewSource)(this.FindResource("perfisViewSource")));

        var perfis = await _db.Perfis
            .ToListAsync();

        perfisViewSource.Source = perfis;
    }

    private void SetStatusBar(string value)
    {
        statusBarLabel.Content = value;

        //statusBarTimer.Enabled = true;
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
}
