using MeuPonto.Models.Contratos;

namespace MeuPonto.Support;

public class GestaoContratosContext
{
    public string NomeContrato { get; private set; }

    public Table Especificacao { get; private set; }

    public Contrato Contrato { get; private set; }

    public string Erro { get; private set; }

    public void Especificar(Table especificacao)
    {
        Especificacao = especificacao;
    }

    public void Contextualizar(Contrato contrato)
    {
        if (contrato == null)
        {
            throw new ArgumentNullException(nameof(contrato));
        }

        Contrato = contrato;

        NomeContrato = contrato.Nome;
    }

    public void CapturarErro(string erro)
    {
        Erro = erro;
    }
}

public class AberturaContratoData
{
    public string Nome { get; set; }
    public bool Ativo { get; set; }
    public string Empregador { get; set; }
    public TimeSpan? Domingo { get; set; }
    public TimeSpan? Segunda { get; set; }
    public TimeSpan? Terca { get; set; }
    public TimeSpan? Quarta { get; set; }
    public TimeSpan? Quinta { get; set; }
    public TimeSpan? Sexta { get; set; }
    public TimeSpan? Sabado { get; set; }
}
