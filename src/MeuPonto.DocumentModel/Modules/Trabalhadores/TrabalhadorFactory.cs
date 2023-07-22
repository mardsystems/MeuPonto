namespace MeuPonto.Modules.Trabalhadores;

public static class TrabalhadorFactory
{
    public static Trabalhador CriaTrabalhador(TransactionContext transaction)
    {
        var trabalhador = new Trabalhador
        {
            Id = transaction.UserId,
            PartitionKey = transaction.UserId.ToString(), 
            CreationDate = transaction.DateTime
        };

        return trabalhador;
    }

    public static void RecontextualizaTrabalhador(this Trabalhador trabalhador, TransactionContext transaction)
    {
        trabalhador.Id = transaction.UserId;
        trabalhador.PartitionKey = transaction.UserId.ToString();
        trabalhador.CreationDate = transaction.DateTime;
    }
}
