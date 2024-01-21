using System.Transactions;
using Timesheet.Models.Pontos;

namespace Timesheet.Features.BackupComprovantes;

public static class BackupComprovantesService
{
    public static Comprovante CriaComprovante(TransactionContext transaction, Guid? id = null)
    {
        var comprovante = new Comprovante
        {
            Id = id ?? Guid.NewGuid(),
            UserId = transaction.UserId,
            PartitionKey = $"{transaction.UserId}",
            CreationDate = transaction.DateTime
        };

        return comprovante;
    }

    public static void RecontextualizaComprovante(this Comprovante comprovante, TransactionContext transaction, Guid? id = null)
    {
        comprovante.Id ??= id ?? Guid.NewGuid();
        comprovante.UserId = transaction.UserId;
        //comprovante.PartitionKey = $"{transaction.UserId}|{comprovante.Ponto.DataHora:yyyy}";
        comprovante.CreationDate ??= transaction.DateTime;
    }

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
