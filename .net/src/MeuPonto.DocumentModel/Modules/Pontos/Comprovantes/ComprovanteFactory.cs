namespace MeuPonto.Modules.Pontos.Comprovantes;

public static class ComprovanteFactory
{
    public static Comprovante CriaComprovante(TransactionContext transaction, Guid? id = null)
    {
        var ponto = new Comprovante
        {
            Id = id ?? Guid.NewGuid(),
            PartitionKey = transaction.UserName,
            CreationDate = transaction.DateTime
        };

        return ponto;
    }
}
