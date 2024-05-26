using MeuPonto.Models.Folhas;

namespace MeuPonto.Support;

public class GestaoFolhasContext
{
    public Table Especificacao { get; private set; }

    public Folha Folha { get; private set; }

    public string Erro { get; private set; }

    public void Especificar(Table especificacao)
    {
        Especificacao = especificacao;
    }

    public void Contextualizar(Folha folha)
    {
        if (folha == null)
        {
            throw new ArgumentNullException(nameof(folha));
        }

        Folha = folha;
    }

    public void CapturarErro(string erro)
    {
        Erro = erro;
    }
}
