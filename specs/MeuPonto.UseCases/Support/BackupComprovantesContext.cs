using MeuPonto.Models.Pontos;

namespace MeuPonto.Support;

public class BackupComprovantesContext
{
    public Stream Imagem { get; private set; }

    public Comprovante Comprovante { get; private set; }

    public Ponto Ponto { get; private set; }

    public BackupComprovantesContext()
    {
        //Comprovante = new Comprovante();

        //var ponto = new Ponto
        //{
        //    Momento = Momento.Entrada,
        //    Pausa = null
        //};

        //Ponto = ponto;
    }

    public void Inicia(Comprovante comprovante)
    {
        Comprovante = comprovante;
    }

    public void Inicia(Ponto ponto)
    {
        Ponto = ponto;
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

public class BackupComprovanteData
{
    
}
