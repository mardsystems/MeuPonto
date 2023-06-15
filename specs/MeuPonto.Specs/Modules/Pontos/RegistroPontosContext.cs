namespace MeuPonto.Modules.Pontos;

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

    public void Define(Concepts.Ponto pontoRegistrado)
    {
        PontoRegistrado = pontoRegistrado;
    }

    public Concepts.Ponto PontoRegistrado { get; set; }
}
