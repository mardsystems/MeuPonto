namespace MeuPonto.Modules.Pontos.Comprovantes;

public static class ComprovanteFacade
{
    public static void ComprovaPonto(this Comprovante comprovante, Pontos.Ponto ponto)
    {
        comprovante.Ponto = new Ponto
        {
            PerfilId = ponto.PerfilId,
            DataHora = ponto.DataHora,
        };

        comprovante.PontoId = ponto.Id;
    }
}
