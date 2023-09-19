namespace MeuPonto.Modules.Pontos.Comprovantes;

public static class ComprovanteFacade
{
    public static void ComprovaPonto(this Comprovante comprovante, Pontos.Ponto ponto)
    {
        comprovante.Ponto = ponto;

        comprovante.PontoId = ponto.Id;
    }
}
