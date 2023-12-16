using MeuPonto.Data;
using MeuPonto.Models.Timesheet.Trabalhadores;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace MeuPonto.Pages.Trabalhadores;

public partial class CadastroTrabalhadoresWindow : Window
{
    private readonly IServiceScope _scope;

    private readonly Data.MeuPontoDbContext _db;

    private CollectionViewSource _trabalhadoresViewSource;

    private ObservableCollection<Trabalhador> _trabalhadores;

    public CadastroTrabalhadoresWindow(IServiceProvider serviceProvider)
    {
        InitializeComponent();

        _scope = serviceProvider.CreateScope();

        _db = _scope.ServiceProvider.GetRequiredService<MeuPontoDbContext>();
    }

    private async void Window_Loaded(object sender, RoutedEventArgs e)
    {
        Cursor = Cursors.Wait;

        _trabalhadoresViewSource = ((CollectionViewSource)(this.FindResource("trabalhadoresViewSource")));

        await _db.Trabalhadores
            .LoadAsync();

        _trabalhadores = _db.Trabalhadores.Local.ToObservableCollection();

        _trabalhadores.CollectionChanged += Trabalhadores_CollectionChanged;

        _trabalhadoresViewSource.Source = _trabalhadores;

        Cursor = null;
    }

    private void Trabalhadores_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.Action == NotifyCollectionChangedAction.Add)
        {
            var trabalhador = e.NewItems[0] as Trabalhador;
        }
    }

    private void SetStatusBar(string value)
    {
        statusBarLabel.Content = value;

        //statusBarTimer.Enabled = true;
    }

    private async void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        //CollectionViewSource trabalhadoresViewSource = ((CollectionViewSource)(this.FindResource("trabalhadoresViewSource")));

        //var observableCollection = (TrabalhadoresCollection)trabalhadoresViewSource.Source;

        trabalhadorViewModelDataGrid.CommitEdit();

        try
        {
            await _db.SaveChangesAsync();

            SetStatusBar("Trabalhadores salvos com sucesso.");
        }
        catch (Exception ex)
        {
            SetStatusBar(ex.Message);
        }
    }

    private void novoTrabalhadorButton_Click(object sender, RoutedEventArgs e)
    {
        var trabalhador = new Trabalhador
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

        //    trabalhador.JornadaTrabalhoSemanalPrevista.Semana.Add(jornadaTrabalhoDiaria);
        //}

        _trabalhadores.Add(trabalhador);
    }

    private void Window_Unloaded(object sender, RoutedEventArgs e)
    {
        //_db.Database.CloseConnection();

        _db.Dispose();

        _scope.Dispose();
    }
}

