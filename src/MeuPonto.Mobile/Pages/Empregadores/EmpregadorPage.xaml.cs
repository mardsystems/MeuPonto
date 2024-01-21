using MeuPonto.Data;
using System.Windows.Input;
using Timesheet.Models.Contratos;

namespace MeuPonto.Pages.Empregadores;

[QueryProperty(nameof(Empregador), "Empregador")]
public partial class EmpregadorPage : ContentPage
{
    private readonly MeuPontoDbContext _db;

    public ICommand ExcluirCommand { get; set; }

    private Empregador _empregador;
    public Empregador Empregador
    {
        get => _empregador;
        set
        {
            _empregador = value;
            OnPropertyChanged();
        }
    }

    public ICommand SalvarCommand { get; set; }

    public EmpregadorPage(MeuPontoDbContext db)
    {
        InitializeComponent();

        _db = db;

        ExcluirCommand = new Command(Excluir);

        Empregador = new Empregador
        {
            Id = Guid.NewGuid()
        };

        SalvarCommand = new Command(Salvar);

        BindingContext = this;
    }

    private void ContentPage_Loaded(object sender, EventArgs e)
    {

    }

    private async void Salvar()
    {
        try
        {
            _db.Empregadores.Add(Empregador);
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
        var yes = await DisplayAlert("Excluir Empregador", "Tem certeza que deseja excluir isso?", "Sim", "Não");

        if (yes)
        {
            try
            {
                var empregador = await _db.Empregadores.FindAsync(Empregador.Id);

                if (empregador != null)
                {
                    _db.Empregadores.Remove(empregador);
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