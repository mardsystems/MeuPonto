using MeuPonto.Data;
using Microsoft.EntityFrameworkCore;
using System.Windows.Input;
using MeuPonto.Models.Contratos;
using MeuPonto.Models.Pontos;

namespace MeuPonto.Pages.Pontos;

[QueryProperty(nameof(Ponto), "Ponto")]
public partial class PontoPage : ContentPage
{
    private readonly MeuPontoDbContext _db;

    public ICommand ExcluirCommand { get; set; }

    private Ponto _ponto;
    public Ponto Ponto
    {
        get => _ponto;
        set
        {
            _ponto = value;
            OnPropertyChanged();
        }
    }

    public DateTime? Data { get; set; }

    public TimeSpan? Hora { get; set; }

    public IEnumerable<Contrato> Contratos { get; set; }

    public ICommand SalvarCommand { get; set; }

    public PontoPage(MeuPontoDbContext db)
    {
        InitializeComponent();

        _db = db;

        ExcluirCommand = new Command(Excluir);

        Ponto = new Ponto
        {
            Id = Guid.NewGuid()
        };

        var agora = DateTime.Now;

        Data = agora.Date;

        Hora = agora.TimeOfDay;

        SalvarCommand = new Command(Salvar);

        BindingContext = this;
    }

    private async void ContentPage_Loaded(object sender, EventArgs e)
    {
        var contratos = await _db.Contratos.ToListAsync();

        Contratos = contratos;
    }

    private async void Salvar()
    {
        try
        {
            _db.Pontos.Add(Ponto);
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
        var yes = await DisplayAlert("Excluir Ponto", "Tem certeza que deseja excluir isso?", "Sim", "N�o");

        if (yes)
        {
            try
            {
                var ponto = await _db.Pontos.FindAsync(Ponto.Id);

                if (ponto != null)
                {
                    _db.Pontos.Remove(ponto);
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