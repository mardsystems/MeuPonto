using MeuPonto.Models.Pontos;

namespace MeuPonto.Support;

public class BackupComprovantesContext
{
    public DateTime? DataHoraPonto { get; private set; }

    public Stream Imagem { get; private set; }

    public Comprovante Comprovante { get; private set; }

    public Ponto Ponto { get; private set; }

    public string Erro { get; private set; }

    public void Contextualizar(Stream imagem)
    {
        Imagem = imagem;
    }

    public void Contextualizar(Comprovante comprovante)
    {
        if (comprovante == null)
        {
            throw new ArgumentNullException(nameof(comprovante));
        }

        Comprovante = comprovante;

        DataHoraPonto = comprovante.Ponto.DataHora;
    }

    public void CapturarErro(string erro)
    {
        Erro = erro;
    }
}

public class BackupComprovanteData
{

}
