using MeuPonto.Models;

namespace MeuPonto.Factories;

public static class FolhaFactory
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
