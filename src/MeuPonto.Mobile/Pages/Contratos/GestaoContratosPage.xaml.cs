using MeuPonto.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Windows.Input;
using MeuPonto.Models.Contratos;

namespace MeuPonto.Pages.Contratos;

public partial class GestaoContratosPage : ContentPage
{
    private readonly MeuPontoDbContext _db;

    public ICommand CriarNovoCommand { get; set; }

    public ObservableCollection<Contrato> Contratos { get; set; }

    public GestaoContratosPage(MeuPontoDbContext db)
    {
        InitializeComponent();

        _db = db;

        Contratos = _db.Contratos.Local.ToObservableCollection();

        contratosSearchHandler.Contratos = Contratos;

        CriarNovoCommand = new Command(CriarNovo);

        BindingContext = this;
    }

    private async void ContentPage_Loaded(object sender, EventArgs e)
    {
        await _db.Contratos
            .LoadAsync();

        //Contratos = _db.Contratos.Local.ToObservableCollection();
    }

    private async void CriarNovo()
    {
        await Shell.Current.GoToAsync($"Contrato");
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        await _db.Contratos
            .LoadAsync();

        var total = Contratos.Count;

        await DisplayAlert("Pronto", $"Carregado {total} registro(s)!", "OK");
    }

    private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        var id = (e.SelectedItem as Contrato).Id;

        var contrato = await _db.Contratos.FirstOrDefaultAsync(x => x.Id == id);

        await Shell.Current.GoToAsync("Contrato", new Dictionary<string, object> { { "Contrato", contrato } });
    }
}