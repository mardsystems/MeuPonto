using MeuPonto.Drivers;
using MeuPonto.Models.Contratos;

namespace MeuPonto.Support;

public class GestaoContratosContext
{
    public string NomeContrato { get; private set; }

    public Table Especificacao { get; private set; }
    
    public AberturaContratoData Data { get; private set; }

    public Contrato Contrato { get; private set; }

    public string Erro { get; private set; }

    public void EspecificaAberturaContrato(Table especificacao)
    {
        Especificacao = especificacao;
    }

    public void GuardaAberturaContrato(AberturaContratoData data)
    {
        Data = data;
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

    public void Recontextualizar(Contrato contrato)
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
