namespace Timesheet.Models.Pontos.Comprovantes;

public static class ComprovanteFacade
{
    public static void ComprovaPonto(this Comprovante comprovante, Ponto ponto)
    {
        comprovante.Ponto = ponto;

        comprovante.PontoId = ponto.Id;
    }
}
