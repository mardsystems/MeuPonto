namespace MeuPonto.Modules.Empregadores;

public static class EmpregadorFactory
{
    public static Empregador CriaEmpregador(TransactionContext transaction, Guid? id = null)
    {
        var empregador = new Empregador
        {
            Id = id ?? Guid.NewGuid(),
            UserId = transaction.UserId,
            PartitionKey = transaction.UserId.ToString(),
            CreationDate = transaction.DateTime
        };

        return empregador;
    }

    public static void RecontextualizaEmpregador(this Empregador empregador, TransactionContext transaction, Guid? id = null)
    {
        empregador.Id ??= id ?? Guid.NewGuid();
        empregador.UserId = transaction.UserId;
        empregador.PartitionKey = transaction.UserId.ToString();
        empregador.CreationDate ??= transaction.DateTime;
    }
}
