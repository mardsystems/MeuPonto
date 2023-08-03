using MeuPonto.Data;
using MeuPonto.Helpers;
using MeuPonto.Modules.Empregadores;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MeuPonto.Modules.Perfis;

public partial class PerfilPage : ContentPage
{
    private readonly MeuPontoDbContext _db;

    public Perfil Perfil { get; set; }

    public ObservableCollection<Empregador> Empregadores { get; set; }

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

        sundayLabel.Text = Perfil.JornadaTrabalhoSemanalPrevista.Semana[0].DiaSemana.Translate();
        mondayLabel.Text = Perfil.JornadaTrabalhoSemanalPrevista.Semana[1].DiaSemana.Translate();

        Empregadores = _db.Empregadores.Local.ToObservableCollection();

        Empregadores.Insert(0, new Empregador { Nome = "" });

        SalvarCommand = new Command(Salvar);

        BindingContext = this;
    }

    private async void ContentPage_Loaded(object sender, EventArgs e)
    {
        await _db.Empregadores
            .LoadAsync();

        //Perfis = _db.Perfis.Local.ToObservableCollection();
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