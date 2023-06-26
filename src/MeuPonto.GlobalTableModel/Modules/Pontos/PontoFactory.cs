namespace MeuPonto.Modules.Pontos;

public static class PontoFactory
{
    public static Ponto CriaPonto(TransactionContext transaction, Guid? id = null)
    {
        var ponto = new Ponto
        {
            Id = id ?? Guid.NewGuid(),
            CreationDate = transaction.DateTime
        };

        return ponto;
    }

    public static void RecontextualizaPonto(this Ponto ponto, TransactionContext transaction, Guid? id = null)
    {
        ponto.Id = ponto.Id ?? id ?? Guid.NewGuid();
        ponto.CreationDate = transaction.DateTime;
    }
}
