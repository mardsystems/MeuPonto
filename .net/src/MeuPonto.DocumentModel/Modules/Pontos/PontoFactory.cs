namespace MeuPonto.Modules.Pontos;

public static class PontoFactory
{
    public static Ponto CriaPonto(TransactionContext transaction)
    {
        var ponto = new Ponto
        {
            Id = transaction.Id,
            PartitionKey = transaction.UserName,
            CreationDate = transaction.DateTime
        };

        return ponto;
    }
}
