using MeuPonto.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MeuPonto.Modules.Pontos.Comprovantes;

public partial class BackupComprovantesPage : ContentPage
{
    private readonly MeuPontoDbContext _db;

    public ICommand CriarNovoCommand { get; set; }

    public ObservableCollection<Comprovante> Comprovantes { get; set; }

    public BackupComprovantesPage(MeuPontoDbContext db)
    {
        InitializeComponent();

        _db = db;

        Comprovantes = _db.Comprovantes.Local.ToObservableCollection();

        CriarNovoCommand = new Command(CriarNovo);

        BindingContext = this;
    }

    private async void ContentPage_Loaded(object sender, EventArgs e)
    {
        await _db.Comprovantes
            .LoadAsync();

        //Comprovantes = _db.Comprovantes.Local.ToObservableCollection();
    }

    private async void CriarNovo()
    {
        await Shell.Current.GoToAsync($"Comprovante");
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        await _db.Comprovantes
            .LoadAsync();

        var total = Comprovantes.Count;

        await DisplayAlert("Pronto", $"Carregado {total} registro(s)!", "OK");
    }

    private async void guardarComprovanteToolbarItem_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("GuardarComprovante");
    }
}