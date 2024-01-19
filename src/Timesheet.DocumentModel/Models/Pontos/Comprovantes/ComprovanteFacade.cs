namespace Timesheet.Models.Pontos.Comprovantes;

public static class ComprovanteFacade
{
    public static void ComprovaPonto(this Comprovante comprovante, Ponto ponto)
    {
        comprovante.Ponto = new PontoRef
        {
            ContratoId = ponto.ContratoId,
            DataHora = ponto.DataHora,
            Contrato = ponto.Contrato,
            MomentoId = ponto.MomentoId,
            PausaId = ponto.PausaId
        };

        comprovante.PartitionKey = $"{comprovante.UserId}|{comprovante.Ponto.DataHora:yyyy}";

        comprovante.PontoId = ponto.Id;
    }
}
