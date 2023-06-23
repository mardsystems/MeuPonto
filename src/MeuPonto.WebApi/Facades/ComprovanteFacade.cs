using MeuPonto.Models;

namespace MeuPonto.Facades;

public static class ComprovanteFacade
{
    public static void ComprovaPonto(this Comprovante comprovante, Ponto ponto)
    {
        comprovante.Ponto = new Ponto
        {
            PerfilId = ponto?.PerfilId,
            DataHora = ponto?.DataHora,
            Perfil = ponto?.Perfil,
            MomentoId = ponto?.MomentoId,
            PausaId = ponto?.PausaId
        };

        comprovante.PontoId = ponto.Id;
    }
}
