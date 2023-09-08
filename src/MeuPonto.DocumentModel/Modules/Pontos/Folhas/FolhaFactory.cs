using MeuPonto.Modules.Trabalhadores;

namespace MeuPonto.Modules.Pontos.Folhas;

public static class FolhaFactory
{
    public static Folha CriaFolha(this TransactionContext transaction, Guid? id = null)
    {
        var folha = new Folha
        {
            Id = id ?? Guid.NewGuid(),
            TrabalhadorId = transaction.UserId,
            PartitionKey = $"{transaction.UserId}",
            CreationDate = transaction.DateTime
        };

        return folha;
    }

    public static void RecontextualizaFolha(this Folha folha, TransactionContext transaction, Guid? id = null)
    {
        folha.Id ??= id ?? Guid.NewGuid();
        folha.TrabalhadorId = transaction.UserId;
        //folha.PartitionKey = $"{transaction.UserId}|{folha.Competencia:yyyy}";
        folha.CreationDate ??= transaction.DateTime;
    }
}
