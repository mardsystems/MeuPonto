using Timesheet.Models.Pontos;

namespace MeuPonto.Support;

public class RegistroPontosContext
{
    public Table Especificacao { get; set; }

    public DateTime DataHora { get; set; }

    public Ponto Ponto { get; set; }

    public string Erro { get; set; }

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

    public void Define(Ponto pontoRegistrado)
    {
        Ponto = pontoRegistrado;
    }
}

public class MarcacaoPontoData
{
    public string Contrato { get; set; }

    public MomentoEnum? MomentoId { get; set; }

    public PausaEnum? PausaId { get; set; }

    public bool Estimado { get; set; }
}
