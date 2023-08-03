using MeuPonto.Data;
using System.Windows.Input;

namespace MeuPonto.Modules.Empregadores;

public partial class EmpregadorPage : ContentPage
{
    private readonly MeuPontoDbContext _db;

    public Empregador Empregador { get; set; }

    public ICommand SalvarCommand { get; set; }

    public EmpregadorPage(MeuPontoDbContext db)
    {
        InitializeComponent();

        _db = db;

        Empregador = new Empregador
        {
            Id = Guid.NewGuid()
        };

        SalvarCommand = new Command(Salvar);

        BindingContext = this;
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
}