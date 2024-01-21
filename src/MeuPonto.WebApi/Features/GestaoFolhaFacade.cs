using MeuPonto.Models;
using System.Transactions;

namespace MeuPonto.Features;

public static class GestaoFolhaFacade
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
