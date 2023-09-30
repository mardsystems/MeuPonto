using MeuPonto.Modules.Pontos.Folhas;

namespace MeuPonto.Modules;

public class HomeDriver
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
