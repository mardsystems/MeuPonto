using MeuPonto.Data;
using Microsoft.EntityFrameworkCore;
using System.Windows.Input;
using Timesheet.Models.Contratos;
using Timesheet.Models.Pontos;

namespace MeuPonto.Pages.Pontos.Comprovantes;

[QueryProperty(nameof(Comprovante), "Comprovante")]
public partial class GuardarComprovantePage : ContentPage
{
    private readonly MeuPontoDbContext _db;

    public ICommand ExcluirCommand { get; set; }

    private Comprovante _ponto;
    public Comprovante Comprovante
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

    public GuardarComprovantePage(MeuPontoDbContext db)
    {
        InitializeComponent();

        _db = db;

        ExcluirCommand = new Command(Excluir);

        Comprovante = new Comprovante
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
            _db.Comprovantes.Add(Comprovante);
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
        var yes = await DisplayAlert("Excluir Comprovante", "Tem certeza que deseja excluir isso?", "Sim", "Não");

        if (yes)
        {
            try
            {
                var ponto = await _db.Comprovantes.FindAsync(Comprovante.Id);

                if (ponto != null)
                {
                    _db.Comprovantes.Remove(ponto);
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