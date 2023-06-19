namespace MeuPonto.Modules.Pontos.Folhas;

public static class FolhaFactory
{
    public static Folha CriaFolha(TransactionContext transaction, Guid? id = null)
    {
        var ponto = new Folha
        {
            Id = id ?? Guid.NewGuid(),
            PartitionKey = transaction.UserName,
            CreationDate = transaction.DateTime
        };

        return ponto;
    }

}
