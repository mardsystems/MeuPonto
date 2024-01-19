using MeuPonto.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using Timesheet.Models.Contratos;

namespace MeuPonto.Pages.Contratos;

public partial class GestaoContratosWindow : Window
{
    private readonly IServiceScope _scope;

    private readonly Data.MeuPontoDbContext _db;

    private CollectionViewSource _contratosViewSource;

    private ObservableCollection<Contrato> _contratos;

    public GestaoContratosWindow(IServiceProvider serviceProvider)
    {
        InitializeComponent();

        _scope = serviceProvider.CreateScope();

        _db = _scope.ServiceProvider.GetRequiredService<MeuPontoDbContext>();
    }

    private async void Window_Loaded(object sender, RoutedEventArgs e)
    {
        Cursor = Cursors.Wait;

        _contratosViewSource = ((CollectionViewSource)(this.FindResource("contratosViewSource")));

        await _db.Contratos
            .LoadAsync();

        _contratos = _db.Contratos.Local.ToObservableCollection();

        _contratos.CollectionChanged += Contratos_CollectionChanged;

        _contratosViewSource.Source = _contratos;

        Cursor = null;
    }

    private void Contratos_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.Action == NotifyCollectionChangedAction.Add)
        {
            var contrato = e.NewItems[0] as Contrato;

            var daysOfWeek = Enum.GetValues<DayOfWeek>();

            foreach (var dayOfWeek in daysOfWeek)
            {
                var jornadaTrabalhoDiaria = new JornadaTrabalhoDiaria
                {
                    DiaSemana = dayOfWeek,
                    Tempo = new TimeSpan(8, 0, 0)
                };

                contrato.JornadaTrabalhoSemanalPrevista.Semana.Add(jornadaTrabalhoDiaria);
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
        //CollectionViewSource contratosViewSource = ((CollectionViewSource)(this.FindResource("contratosViewSource")));

        //var observableCollection = (ContratosCollection)contratosViewSource.Source;

        contratoViewModelDataGrid.CommitEdit();

        try
        {
            await _db.SaveChangesAsync();

            SetStatusBar("Contratos salvos com sucesso.");
        }
        catch (Exception ex)
        {
            SetStatusBar(ex.Message);
        }
    }

    private void novoContratoButton_Click(object sender, RoutedEventArgs e)
    {
        var contrato = new Contrato
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

        //    contrato.JornadaTrabalhoSemanalPrevista.Semana.Add(jornadaTrabalhoDiaria);
        //}

        _contratos.Add(contrato);
    }

    private void Window_Unloaded(object sender, RoutedEventArgs e)
    {
        //_db.Database.CloseConnection();

        _db.Dispose();

        _scope.Dispose();
    }
}

