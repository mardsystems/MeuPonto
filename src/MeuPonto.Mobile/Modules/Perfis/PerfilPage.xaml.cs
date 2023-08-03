using MeuPonto.Data;
using MeuPonto.Helpers;
using MeuPonto.Modules.Empregadores;
using Microsoft.EntityFrameworkCore;
using System.Windows.Input;

namespace MeuPonto.Modules.Perfis;

[QueryProperty(nameof(Perfil), "Perfil")]
public partial class PerfilPage : ContentPage
{
    private readonly MeuPontoDbContext _db;

    private Perfil _perfil;
    public Perfil Perfil
    {
        get => _perfil;
        set
        {
            _perfil = value;
            OnPropertyChanged();
        }
    }

    public IEnumerable<Empregador> Empregadores { get; set; }

    public ICommand SalvarCommand { get; set; }

    public PerfilPage(MeuPontoDbContext db)
    {
        InitializeComponent();

        _db = db;

        Perfil = new Perfil
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

            Perfil.JornadaTrabalhoSemanalPrevista.Semana.Add(jornadaTrabalhoDiaria);
        }

        sundayLabel.Text = Perfil.JornadaTrabalhoSemanalPrevista.Semana[(int)DayOfWeek.Sunday].DiaSemana.Translate();
        mondayLabel.Text = Perfil.JornadaTrabalhoSemanalPrevista.Semana[(int)DayOfWeek.Monday].DiaSemana.Translate();
        tuesdayLabel.Text = Perfil.JornadaTrabalhoSemanalPrevista.Semana[(int)DayOfWeek.Tuesday].DiaSemana.Translate();
        wednesdayLabel.Text = Perfil.JornadaTrabalhoSemanalPrevista.Semana[(int)DayOfWeek.Wednesday].DiaSemana.Translate();
        thursdayLabel.Text = Perfil.JornadaTrabalhoSemanalPrevista.Semana[(int)DayOfWeek.Thursday].DiaSemana.Translate();
        fridayLabel.Text = Perfil.JornadaTrabalhoSemanalPrevista.Semana[(int)DayOfWeek.Friday].DiaSemana.Translate();
        saturdayLabel.Text = Perfil.JornadaTrabalhoSemanalPrevista.Semana[(int)DayOfWeek.Saturday].DiaSemana.Translate();

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
            _db.Perfis.Add(Perfil);
            await _db.SaveChangesAsync();

            await Shell.Current.GoToAsync("..");
        }
        catch (Exception _)
        {
            throw;
        }
    }
}