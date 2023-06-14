using MeuPonto.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Data;

namespace MeuPonto.Modules.Perfis;

public partial class CadastroPerfisWindow : Window
{
    private readonly IServiceScope _scope;

    private readonly Data.MeuPontoDbContext _db;

    private CollectionViewSource _perfisViewSource;

    private ObservableCollection<Perfil> _perfis;

    public CadastroPerfisWindow(IServiceProvider serviceProvider)
    {
        InitializeComponent();

        _scope = serviceProvider.CreateScope();

        _db = _scope.ServiceProvider.GetRequiredService<MeuPontoDbContext>();
    }

    private async void Window_Loaded(object sender, RoutedEventArgs e)
    {
        _perfisViewSource = ((CollectionViewSource)(this.FindResource("perfisViewSource")));

        await _db.Perfis
            .LoadAsync();

        _perfis = _db.Perfis.Local.ToObservableCollection();

        _perfis.CollectionChanged += Perfis_CollectionChanged;

        _perfisViewSource.Source = _perfis;
    }

    private void Perfis_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.Action == NotifyCollectionChangedAction.Add)
        {
            var perfil = e.NewItems[0] as Perfil;

            var daysOfWeek = Enum.GetValues<DayOfWeek>();

            foreach (var dayOfWeek in daysOfWeek)
            {
                var jornadaTrabalhoDiaria = new JornadaTrabalhoDiaria
                {
                    DiaSemana = dayOfWeek,
                    Tempo = new TimeSpan(8, 0, 0)
                };

                perfil.JornadaTrabalhoSemanalPrevista.Semana.Add(jornadaTrabalhoDiaria);
            }
        }
    }

    private void SetStatusBar(string value)
    {
        statusBarLabel.Content = value;

        //statusBarTimer.Enabled = true;
    }

    private async void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        //CollectionViewSource perfisViewSource = ((CollectionViewSource)(this.FindResource("perfisViewSource")));

        //var observableCollection = (PerfisCollection)perfisViewSource.Source;

        perfilViewModelDataGrid.CommitEdit();

        try
        {
            await _db.SaveChangesAsync();

            SetStatusBar("Perfis salvos com sucesso.");
        }
        catch (Exception ex)
        {
            SetStatusBar(ex.Message);
        }
    }

    private void novoPerfilButton_Click(object sender, RoutedEventArgs e)
    {
        var perfil = new Perfil
        {
            CreationDate = DateTime.Now,
        };

        //var daysOfWeek = Enum.GetValues<DayOfWeek>();

        //foreach (var dayOfWeek in daysOfWeek)
        //{
        //    var jornadaTrabalhoDiaria = new JornadaTrabalhoDiaria
        //    {
        //        DiaSemana = dayOfWeek,
        //        Tempo = new TimeSpan(8, 0, 0)
        //    };

        //    perfil.JornadaTrabalhoSemanalPrevista.Semana.Add(jornadaTrabalhoDiaria);
        //}

        _perfis.Add(perfil);
    }

    private void Window_Unloaded(object sender, RoutedEventArgs e)
    {
        _db.Database.CloseConnection();

        _db.Dispose();

        _scope.Dispose();
    }
}

