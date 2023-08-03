using MeuPonto.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace MeuPonto.Modules.Pontos;

public partial class RegistroPontosPage : ContentPage
{
    private readonly MeuPontoDbContext _db;

    public ObservableCollection<Ponto> Pontos { get; set; }

    public RegistroPontosPage(MeuPontoDbContext db)
    {
        InitializeComponent();

        _db = db;

        Pontos = _db.Pontos.Local.ToObservableCollection();

        BindingContext = this;
    }

    private async void ContentPage_Loaded(object sender, EventArgs e)
    {
        await _db.Pontos
            .LoadAsync();

        //Pontos = _db.Pontos.Local.ToObservableCollection();
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        await _db.Pontos
            .LoadAsync();

        var total = Pontos.Count;

        await DisplayAlert("Pronto", $"Carregado {total} registro(s)!", "OK");
    }
}