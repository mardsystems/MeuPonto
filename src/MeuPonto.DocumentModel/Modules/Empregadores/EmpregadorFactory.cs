using MeuPonto.Modules.Trabalhadores;

namespace MeuPonto.Modules.Empregadores;

public static class EmpregadorFactory
{
    public static Empregador CriaEmpregador(this Trabalhador trabalhador, TransactionContext transaction, Guid? id = null)
    {
        var empregador = new Empregador
        {
            Id = id ?? Guid.NewGuid(),
            TrabalhadorId = trabalhador.Id,
            PartitionKey = trabalhador.Id.ToString(),
            CreationDate = transaction.DateTime
        };

        return empregador;
    }

    public static void RecontextualizaEmpregador(this Trabalhador trabalhador, Empregador empregador, TransactionContext transaction, Guid? id = null)
    {
        empregador.Id ??= id ?? Guid.NewGuid();
        empregador.TrabalhadorId = trabalhador.Id;
        empregador.PartitionKey = trabalhador.Id.ToString();
        empregador.CreationDate ??= transaction.DateTime;
    }
}
