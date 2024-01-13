using MeuPonto.Models;

namespace MeuPonto.Facades;

public static class ComprovanteFacade
{
    public static void ComprovaPonto(this Comprovante comprovante, Ponto ponto)
    {
        comprovante.Ponto = new Ponto
        {
            ContratoId = ponto?.ContratoId,
            DataHora = ponto?.DataHora,
            Contrato = ponto?.Contrato,
            MomentoId = ponto?.MomentoId,
            PausaId = ponto?.PausaId
        };

        comprovante.PontoId = ponto.Id;
    }
}
