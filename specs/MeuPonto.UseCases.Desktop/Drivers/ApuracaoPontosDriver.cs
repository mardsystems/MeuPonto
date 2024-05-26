using MeuPonto.Models.Folhas;

namespace MeuPonto.Drivers;

public class ApuracaoPontosDriver
{
    public void GoTo()
    {

    }

    public Folha ApurarFolha(Folha folhaAberta)
    {
        GoTo();

        var folhaApurada = IdentificaFolhaParaApuracao();

        return folhaApurada;
    }

    private Folha IdentificaFolhaParaApuracao()
    {
        var folhaApurada = new Folha
        {
            ApuracaoMensal = new ApuracaoMensal
            {

            },
        };

        return folhaApurada;
    }
}
