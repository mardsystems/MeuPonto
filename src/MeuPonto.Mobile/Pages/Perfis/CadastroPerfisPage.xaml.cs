using MeuPonto.Data;
using MeuPonto.Models.Timesheet.Perfis;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MeuPonto.Pages.Perfis;

public partial class CadastroPerfisPage : ContentPage
{
    private readonly MeuPontoDbContext _db;

    public ICommand CriarNovoCommand { get; set; }

    public ObservableCollection<Perfil> Perfis { get; set; }

    public CadastroPerfisPage(MeuPontoDbContext db)
    {
        InitializeComponent();

        _db = db;

        Perfis = _db.Perfis.Local.ToObservableCollection();

        perfisSearchHandler.Perfis = Perfis;

        CriarNovoCommand = new Command(CriarNovo);

        BindingContext = this;
    }

    private async void ContentPage_Loaded(object sender, EventArgs e)
    {
        await _db.Perfis
            .LoadAsync();

        //Perfis = _db.Perfis.Local.ToObservableCollection();
    }

    private async void CriarNovo()
    {
        await Shell.Current.GoToAsync($"Perfil");
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        await _db.Perfis
            .LoadAsync();

        var total = Perfis.Count;

        await DisplayAlert("Pronto", $"Carregado {total} registro(s)!", "OK");
    }

    private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        var id = (e.SelectedItem as Perfil).Id;

        var perfil = await _db.Perfis.FirstOrDefaultAsync(x => x.Id == id);

        await Shell.Current.GoToAsync("Perfil", new Dictionary<string, object> { { "Perfil", perfil } });
    }
}