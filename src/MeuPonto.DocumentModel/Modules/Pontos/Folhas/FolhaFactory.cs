namespace MeuPonto.Modules.Pontos.Folhas;

public static class FolhaFactory
{
    public static Folha CriaFolha(TransactionContext transaction, Guid? id = null)
    {
        var folha = new Folha
        {
            Id = id ?? Guid.NewGuid(),
            PartitionKey = transaction.UserId.ToString(),
            CreationDate = transaction.DateTime
        };

        return folha;
    }

    public static void RecontextualizaFolha(this Folha folha, TransactionContext transaction, Guid? id = null)
    {
        folha.Id = folha.Id ?? id ?? Guid.NewGuid();
        folha.PartitionKey = transaction.UserId.ToString();
        folha.CreationDate = transaction.DateTime;
    }
}
