using Timesheet.Models.Pontos;

namespace MeuPonto.Support;

public class RegistroPontosContext
{
    public RegistroPontosContext()
    {
        //var ponto = new Ponto
        //{
        //    Momento = Momento.Entrada,
        //    Pausa = null
        //};

        //Ponto = ponto;
    }

    public void Inicia(Ponto ponto)
    {
        Ponto = ponto;
    }

    public DateTime DataHora { get; set; }

    public Ponto Ponto { get; set; }

    public void Define(Ponto pontoRegistrado)
    {
        PontoRegistrado = pontoRegistrado;
    }

    public Ponto PontoRegistrado { get; set; }
}
