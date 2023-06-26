namespace MeuPonto.Modules.Pontos.Comprovantes;

public static class ComprovanteFactory
{
    public static Comprovante CriaComprovante(TransactionContext transaction, Guid? id = null)
    {
        var comprovante = new Comprovante
        {
            Id = id ?? Guid.NewGuid(),
            PartitionKey = transaction.UserName,
            CreationDate = transaction.DateTime
        };

        return comprovante;
    }

    public static void RecontextualizaComprovante(this Comprovante comprovante, TransactionContext transaction, Guid? id = null)
    {
        comprovante.Id = comprovante.Id ?? id ?? Guid.NewGuid();
        comprovante.PartitionKey = transaction.UserName;
        comprovante.CreationDate = transaction.DateTime;
    }
}
