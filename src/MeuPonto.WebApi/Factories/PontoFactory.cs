using MeuPonto.Models;

namespace MeuPonto.Factories;

public static class PontoFactory
{
    public static Ponto CriaPonto(TransactionContext transaction)
    {
        var ponto = new Ponto
        {
            UserId = transaction.UserId,
            CreationDate = transaction.DateTime
        };

        return ponto;
    }

    public static void RecontextualizaPonto(this Ponto ponto, TransactionContext transaction)
    {
        ponto.UserId = transaction.UserId;
        ponto.CreationDate = transaction.DateTime;
    }
}
