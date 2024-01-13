using MeuPonto.Models;

namespace MeuPonto.Factories;

public static class ContratoFactory
{
    public static Contrato CriaContrato(TransactionContext transaction)
    {
        var contrato = new Contrato
        {
            UserId = transaction.UserId,
            CreationDate = transaction.DateTime
        };

        return contrato;
    }
    
    public static void RecontextualizaContrato(this Contrato contrato, TransactionContext transaction, Guid? id = null)
    {
        contrato.UserId = transaction.UserId;
        contrato.CreationDate = transaction.DateTime;
    }
}
