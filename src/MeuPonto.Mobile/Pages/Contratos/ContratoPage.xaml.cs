using MeuPonto.Data;
using MeuPonto.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Windows.Input;
using Timesheet.Models.Contratos;
using Timesheet.Models.Contratos.Empregadores;

namespace MeuPonto.Pages.Contratos;

[QueryProperty(nameof(Contrato), "Contrato")]
public partial class ContratoPage : ContentPage
{
    private readonly MeuPontoDbContext _db;

    public ICommand ExcluirCommand { get; set; }

    private Contrato _contrato;
    public Contrato Contrato
    {
        get => _contrato;
        set
        {
            _contrato = value;
            OnPropertyChanged();
        }
    }

    public IEnumerable<Empregador> Empregadores { get; set; }

    public ICommand SalvarCommand { get; set; }

    public ContratoPage(MeuPontoDbContext db)
    {
        InitializeComponent();

        _db = db;

        ExcluirCommand = new Command(Excluir);

        Contrato = new Contrato
        {
            Id = Guid.NewGuid(),
            Ativo = false,
        };

        var daysOfWeek = Enum.GetValues<DayOfWeek>();

        foreach (var dayOfWeek in daysOfWeek)
        {
            var jornadaTrabalhoDiaria = new JornadaTrabalhoDiaria
            {
                DiaSemana = dayOfWeek,
                Tempo = new TimeSpan(8, 0, 0)
            };

            Contrato.JornadaTrabalhoSemanalPrevista.Semana.Add(jornadaTrabalhoDiaria);
        }

        sundayLabel.Text = Contrato.JornadaTrabalhoSemanalPrevista.Semana[(int)DayOfWeek.Sunday].DiaSemana.Translate();
        mondayLabel.Text = Contrato.JornadaTrabalhoSemanalPrevista.Semana[(int)DayOfWeek.Monday].DiaSemana.Translate();
        tuesdayLabel.Text = Contrato.JornadaTrabalhoSemanalPrevista.Semana[(int)DayOfWeek.Tuesday].DiaSemana.Translate();
        wednesdayLabel.Text = Contrato.JornadaTrabalhoSemanalPrevista.Semana[(int)DayOfWeek.Wednesday].DiaSemana.Translate();
        thursdayLabel.Text = Contrato.JornadaTrabalhoSemanalPrevista.Semana[(int)DayOfWeek.Thursday].DiaSemana.Translate();
        fridayLabel.Text = Contrato.JornadaTrabalhoSemanalPrevista.Semana[(int)DayOfWeek.Friday].DiaSemana.Translate();
        saturdayLabel.Text = Contrato.JornadaTrabalhoSemanalPrevista.Semana[(int)DayOfWeek.Saturday].DiaSemana.Translate();

        SalvarCommand = new Command(Salvar);

        BindingContext = this;
    }

    private async void ContentPage_Loaded(object sender, EventArgs e)
    {
        var empregadores = await _db.Empregadores.ToListAsync();

        empregadores.Insert(0, new Empregador { Nome = "" });

        Empregadores = empregadores;
    }

    private async void Salvar()
    {
        try
        {
            _db.Contratos.Add(Contrato);
            await _db.SaveChangesAsync();

            await Shell.Current.GoToAsync("..");
        }
        catch (Exception _)
        {
            throw;
        }
    }

    private async void Excluir()
    {
        var yes = await DisplayAlert("Excluir Contrato", "Tem certeza que deseja excluir isso?", "Sim", "Não");

        if (yes)
        {
            try
            {
                var contrato = await _db.Contratos.FindAsync(Contrato.Id);

                if (contrato != null)
                {
                    _db.Contratos.Remove(contrato);
                    await _db.SaveChangesAsync();
                }

                await Shell.Current.GoToAsync("..");
            }
            catch (Exception _)
            {
                throw;
            }

            await Shell.Current.GoToAsync("..");
        }
    }
}