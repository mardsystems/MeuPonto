namespace MeuPonto.Modules.Pontos.Comprovantes;

public class BackupComprovantesContext
{
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

    public void Inicia(Pontos.Ponto ponto)
    {
        Ponto = ponto;
    }

    public void Define(Stream imagem)
    {
        Imagem = imagem;
    }

    public Stream Imagem { get; private set; }

    public Comprovante Comprovante { get; private set; }

    public Pontos.Ponto Ponto { get; private set; }

    public void Define(Concepts.Comprovante comprovante)
    {
        ComprovanteGuardado = comprovante;
    }

    public Concepts.Comprovante ComprovanteGuardado { get; private set; }
}
