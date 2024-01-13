using MeuPonto.Models.Timesheet.Pontos;
using MeuPonto.Models.Timesheet.Pontos.Comprovantes;

namespace MeuPonto.Support;

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

    public void Inicia(Ponto ponto)
    {
        Ponto = ponto;
    }

    public void Define(Stream imagem)
    {
        Imagem = imagem;
    }

    public Stream Imagem { get; private set; }

    public Comprovante Comprovante { get; private set; }

    public Ponto Ponto { get; private set; }

    public void Define(Comprovante comprovante)
    {
        ComprovanteGuardado = comprovante;
    }

    public Comprovante ComprovanteGuardado { get; private set; }
}
