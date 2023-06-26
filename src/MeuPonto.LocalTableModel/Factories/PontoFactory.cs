using MeuPonto.Models;

namespace MeuPonto.Factories;

public static class PontoFactory
{
    public static Ponto CriaPonto(TransactionContext transaction)
    {
        var ponto = new Ponto
        {
            Id = null,
            CreationDate = transaction.DateTime
        };

        return ponto;
    }
}
