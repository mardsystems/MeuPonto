using MeuPonto.Models.Pontos;

namespace MeuPonto.Support;

public class RegistroPontosContext
{
    public Table Especificacao { get; set; }

    public DateTime DataHora { get; set; }

    public Ponto Ponto { get; set; }

    public Stream Imagem { get; private set; }

    public Comprovante Comprovante { get; private set; }

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

    public void Inicia(Comprovante comprovante)
    {
        Comprovante = comprovante;
    }

    public void Define(Ponto pontoRegistrado)
    {
        Ponto = pontoRegistrado;
    }

    public void Define(Stream imagem)
    {
        Imagem = imagem;
    }

    public void Define(Comprovante comprovante)
    {
        Comprovante = comprovante;
    }
}

public class RegistroPontoData
{
    public DateTime? DataHora { get; set; }

    public string Contrato { get; set; }

    public MomentoEnum? MomentoId { get; set; }

    public PausaEnum? PausaId { get; set; }

    public bool Estimado { get; set; }
}

public class MarcacaoPontoData
{
    public string Contrato { get; set; }

    public MomentoEnum? MomentoId { get; set; }

    public PausaEnum? PausaId { get; set; }
}
