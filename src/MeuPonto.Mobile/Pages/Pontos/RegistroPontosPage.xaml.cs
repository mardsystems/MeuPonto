using MeuPonto.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Timesheet.Models.Pontos;

namespace MeuPonto.Pages.Pontos;

public partial class RegistroPontosPage : ContentPage
{
    private readonly MeuPontoDbContext _db;

    public ICommand CriarNovoCommand { get; set; }

    public ObservableCollection<Ponto> Pontos { get; set; }

    public RegistroPontosPage(MeuPontoDbContext db)
    {
        InitializeComponent();

        _db = db;

        Pontos = _db.Pontos.Local.ToObservableCollection();

        CriarNovoCommand = new Command(CriarNovo);

        BindingContext = this;
    }

    private async void ContentPage_Loaded(object sender, EventArgs e)
    {
        await _db.Pontos
            .LoadAsync();

        //Pontos = _db.Pontos.Local.ToObservableCollection();
    }

    private async void CriarNovo()
    {
        await Shell.Current.GoToAsync($"Ponto");
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        await _db.Pontos
            .LoadAsync();

        var total = Pontos.Count;

        await DisplayAlert("Pronto", $"Carregado {total} registro(s)!", "OK");
    }

    private async void folhasToolbarItem_Clicked(object sender, EventArgs e)
    {
        
    }

    private async void comprovantesToolbarItem_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("Comprovantes");
    }
}