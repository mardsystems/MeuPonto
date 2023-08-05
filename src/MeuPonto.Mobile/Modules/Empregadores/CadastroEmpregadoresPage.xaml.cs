using MeuPonto.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MeuPonto.Modules.Empregadores;

public partial class CadastroEmpregadoresPage : ContentPage
{
    private readonly MeuPontoDbContext _db;

    public ICommand CriarNovoCommand { get; set; }

    public ObservableCollection<Empregador> Empregadores { get; set; }

    public CadastroEmpregadoresPage(MeuPontoDbContext db)
    {
        InitializeComponent();

        _db = db;

        Empregadores = _db.Empregadores.Local.ToObservableCollection();

        empregadoresSearchHandler.Empregadores = Empregadores;

        CriarNovoCommand = new Command(CriarNovo);

        BindingContext = this;
    }

    private async void ContentPage_Loaded(object sender, EventArgs e)
    {
        await _db.Empregadores
            .LoadAsync();

        //Empregadores = _db.Empregadores.Local.ToObservableCollection();
    }

    private async void CriarNovo()
    {
        await Shell.Current.GoToAsync($"Empregador");
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        await _db.Empregadores
            .LoadAsync();

        var total = Empregadores.Count;

        await DisplayAlert("Pronto", $"Carregado {total} registro(s)!", "OK");
    }

    private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        var id = (e.SelectedItem as Empregador).Id;

        var empregador = await _db.Empregadores.FirstOrDefaultAsync(x => x.Id == id);

        await Shell.Current.GoToAsync("Empregador", new Dictionary<string, object> { { "Empregador", empregador } });
    }
}