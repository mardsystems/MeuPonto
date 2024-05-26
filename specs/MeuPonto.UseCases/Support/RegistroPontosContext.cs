using MeuPonto.Models.Pontos;

namespace MeuPonto.Support;

public class RegistroPontosContext
{
    public DateTime? DataHoraPonto { get; private set; }

    public Table Especificacao { get; private set; }

    public Ponto Ponto { get; private set; }

    public string Erro { get; private set; }

    public void Especificar(Table especificacao)
    {
        Especificacao = especificacao;
    }

    public void Contextualizar(Ponto ponto)
    {
        if (ponto == null)
        {
            throw new ArgumentNullException(nameof(ponto));
        }

        Ponto = ponto;

        DataHoraPonto = ponto.DataHora;
    }

    public void CapturarErro(string erro)
    {
        Erro = erro;
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
