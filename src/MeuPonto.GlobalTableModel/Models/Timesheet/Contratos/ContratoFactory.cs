namespace MeuPonto.Models.Timesheet.Contratos;

public static class ContratoFactory
{
    public static Contrato CriaContrato(TransactionContext transaction, Guid? id = null)
    {
        var contrato = new Contrato
        {
            Id = id ?? Guid.NewGuid(),
            UserId = transaction.UserId,
            CreationDate = transaction.DateTime
        };

        return contrato;
    }

    public static void RecontextualizaContrato(this Contrato contrato, TransactionContext transaction, Guid? id = null)
    {
        contrato.Id = contrato.Id ?? id ?? Guid.NewGuid();
        contrato.UserId = transaction.UserId;
        contrato.CreationDate = transaction.DateTime;
    }
}
