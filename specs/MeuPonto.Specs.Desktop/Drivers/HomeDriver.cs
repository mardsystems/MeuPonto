using Timesheet.Models.Folhas;

namespace MeuPonto.Drivers;

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
