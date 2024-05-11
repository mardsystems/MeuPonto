using MeuPonto.Models.Pontos;

namespace MeuPonto.Drivers;

public class BackupComprovantesDriver
{
    public void GoTo()
    {

    }

    public Comprovante EscanearComprovante(Stream imagem, Comprovante comprovante, Ponto ponto)
    {
        GoTo();

        var comprovanteEscaneado = ObtemResultadoEscanearComprovante();

        return comprovanteEscaneado;
    }

    private Comprovante ObtemResultadoEscanearComprovante()
    {
        var comprovanteEscaneado = new Comprovante
        {
            Ponto = new()
            {
                Contrato = new()
                {

                },
            }
        };

        return comprovanteEscaneado;
    }

    public Comprovante GuardarComprovante(Stream imagem, Comprovante comprovante, Ponto ponto)
    {
        GoTo();

        var comprovanteGuardado = ObtemDetalhes();

        return comprovanteGuardado;
    }

    private Comprovante ObtemDetalhes()
    {
        var comprovanteGuardado = new Comprovante
        {
            Ponto = new()
            {
                Contrato = new()
                {

                },
            }
        };

        return comprovanteGuardado;
    }
}
