using MeuPonto.Data;
using MeuPonto.Models.Timesheet.Empregadores;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace MeuPonto.Pages.Empregadores;

public partial class CadastroEmpregadoresWindow : Window
{
    private readonly IServiceScope _scope;

    private readonly Data.MeuPontoDbContext _db;

    private CollectionViewSource _empregadoresViewSource;

    private ObservableCollection<Empregador> _empregadores;

    public CadastroEmpregadoresWindow(IServiceProvider serviceProvider)
    {
        InitializeComponent();

        _scope = serviceProvider.CreateScope();

        _db = _scope.ServiceProvider.GetRequiredService<MeuPontoDbContext>();
    }

    private async void Window_Loaded(object sender, RoutedEventArgs e)
    {
        Cursor = Cursors.Wait;

        _empregadoresViewSource = ((CollectionViewSource)(this.FindResource("empregadoresViewSource")));

        await _db.Empregadores
            .LoadAsync();

        _empregadores = _db.Empregadores.Local.ToObservableCollection();

        _empregadores.CollectionChanged += Empregadores_CollectionChanged;

        _empregadoresViewSource.Source = _empregadores;

        Cursor = null;
    }

    private void Empregadores_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.Action == NotifyCollectionChangedAction.Add)
        {
            var empregador = e.NewItems[0] as Empregador;
        }
    }

    private void SetStatusBar(string value)
    {
        statusBarLabel.Content = value;

        //statusBarTimer.Enabled = true;
    }

    private async void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        //CollectionViewSource empregadoresViewSource = ((CollectionViewSource)(this.FindResource("empregadoresViewSource")));

        //var observableCollection = (EmpregadoresCollection)empregadoresViewSource.Source;

        empregadorViewModelDataGrid.CommitEdit();

        try
        {
            await _db.SaveChangesAsync();

            SetStatusBar("Empregadores salvos com sucesso.");
        }
        catch (Exception ex)
        {
            SetStatusBar(ex.Message);
        }
    }

    private void novoEmpregadorButton_Click(object sender, RoutedEventArgs e)
    {
        var empregador = new Empregador
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

        //    empregador.JornadaTrabalhoSemanalPrevista.Semana.Add(jornadaTrabalhoDiaria);
        //}

        _empregadores.Add(empregador);
    }

    private void Window_Unloaded(object sender, RoutedEventArgs e)
    {
        //_db.Database.CloseConnection();

        _db.Dispose();

        _scope.Dispose();
    }
}

