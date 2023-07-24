using MeuPonto.Modules.Trabalhadores;

namespace MeuPonto.Modules.Pontos.Folhas;

public static class FolhaFactory
{
    public static Folha CriaFolha(this Trabalhador trabalhador, TransactionContext transaction, Guid? id = null)
    {
        var folha = new Folha
        {
            Id = id ?? Guid.NewGuid(),
            TrabalhadorId = trabalhador.Id,
            PartitionKey = $"{trabalhador.Id}",
            CreationDate = transaction.DateTime
        };

        return folha;
    }

    public static void RecontextualizaFolha(this Trabalhador trabalhador, Folha folha, TransactionContext transaction, Guid? id = null)
    {
        folha.Id = folha.Id ?? id ?? Guid.NewGuid();
        folha.TrabalhadorId = trabalhador.Id;
        //folha.PartitionKey = $"{trabalhador.Id}|{folha.Competencia:yyyy}";
        folha.CreationDate = transaction.DateTime;
    }
}
