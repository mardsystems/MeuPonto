using System.Transactions;

namespace Timesheet.Models.Pontos;

public static class PontoFactory
{
    public static Ponto CriaPonto(TransactionContext transaction, Guid? id = null)
    {
        var ponto = new Ponto
        {
            Id = id ?? Guid.NewGuid(),
            UserId = transaction.UserId,
            PartitionKey = $"{transaction.UserId}",
            CreationDate = transaction.DateTime
        };

        return ponto;
    }

    public static void RecontextualizaPonto(this Ponto ponto, TransactionContext transaction, Guid? id = null)
    {
        ponto.Id ??= id ?? Guid.NewGuid();
        ponto.UserId = transaction.UserId;
        ponto.PartitionKey = $"{transaction.UserId}|{ponto.DataHora:yyyy}";
        ponto.CreationDate ??= transaction.DateTime;
    }
}
