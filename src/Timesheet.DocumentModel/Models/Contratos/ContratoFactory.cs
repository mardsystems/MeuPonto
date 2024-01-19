using System.Transactions;

namespace Timesheet.Models.Contratos;

public static class ContratoFactory
{
    public static Contrato CriaContrato(TransactionContext transaction, Guid? id = null)
    {
        var contrato = new Contrato
        {
            Id = id ?? Guid.NewGuid(),
            UserId = transaction.UserId,
            PartitionKey = transaction.UserId.ToString(),
            CreationDate = transaction.DateTime
        };

        return contrato;
    }

    public static void RecontextualizaContrato(this Contrato contrato, TransactionContext transaction, Guid? id = null)
    {
        contrato.Id ??= id ?? Guid.NewGuid();
        contrato.UserId = transaction.UserId;
        contrato.PartitionKey = transaction.UserId.ToString();
        contrato.CreationDate ??= transaction.DateTime;
    }
}
