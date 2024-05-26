using MeuPonto.Models.Contratos;

namespace MeuPonto.Support;

public class CadastroEmpregadoresContext
{
    public string NomeEmpregador { get; private set; }

    public Table Especificacao { get; private set; }

    public Empregador Empregador { get; private set; }

    public string Erro { get; private set; }

    public void Especificar(Table especificacao)
    {
        Especificacao = especificacao;
    }

    public void Contextualizar(Empregador empregador)
    {
        if (empregador == null)
        {
            throw new ArgumentNullException(nameof(empregador));
        }

        Empregador = empregador;

        NomeEmpregador = empregador.Nome;
    }

    public void CapturarErro(string erro)
    {
        Erro = erro;
    }
}

public class CadastroEmpregadorData
{
    public string Nome { get; set; }    
}
