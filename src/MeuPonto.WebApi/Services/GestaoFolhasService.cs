using MeuPonto.Models;
using System.Transactions;

namespace MeuPonto.Services;

public static class GestaoFolhasService
{
    public static Folha CriaFolha(TransactionContext transaction)
    {
        var folha = new Folha
        {
            UserId = transaction.UserId,
            CreationDate = transaction.DateTime
        };

        return folha;
    }

    public static void RecontextualizaFolha(this Folha folha, TransactionContext transaction)
    {
        folha.UserId = transaction.UserId;
        folha.CreationDate = transaction.DateTime;
    }
}
