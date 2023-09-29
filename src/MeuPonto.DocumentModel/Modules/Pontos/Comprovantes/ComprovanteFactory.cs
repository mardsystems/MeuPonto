namespace MeuPonto.Modules.Pontos.Comprovantes;

public static class ComprovanteFactory
{
    public static Comprovante CriaComprovante(TransactionContext transaction, Guid? id = null)
    {
        var comprovante = new Comprovante
        {
            Id = id ?? Guid.NewGuid(),
            UserId = transaction.UserId,
            PartitionKey = $"{transaction.UserId}",
            CreationDate = transaction.DateTime
        };

        return comprovante;
    }

    public static void RecontextualizaComprovante(this Comprovante comprovante, TransactionContext transaction, Guid? id = null)
    {
        comprovante.Id ??= id ?? Guid.NewGuid();
        comprovante.UserId = transaction.UserId;
        //comprovante.PartitionKey = $"{transaction.UserId}|{comprovante.Ponto.DataHora:yyyy}";
        comprovante.CreationDate ??= transaction.DateTime;
    }
}
