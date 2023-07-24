using MeuPonto.Modules.Trabalhadores;

namespace MeuPonto.Modules.Pontos.Comprovantes;

public static class ComprovanteFactory
{
    public static Comprovante CriaComprovante(this Trabalhador trabalhador, TransactionContext transaction, Guid? id = null)
    {
        var comprovante = new Comprovante
        {
            Id = id ?? Guid.NewGuid(),
            TrabalhadorId = trabalhador.Id,
            PartitionKey = $"{trabalhador.Id}",
            CreationDate = transaction.DateTime
        };

        return comprovante;
    }

    public static void RecontextualizaComprovante(this Trabalhador trabalhador, Comprovante comprovante, TransactionContext transaction, Guid? id = null)
    {
        comprovante.Id ??= id ?? Guid.NewGuid();
        comprovante.TrabalhadorId = trabalhador.Id;
        //comprovante.PartitionKey = $"{trabalhador.Id}|{comprovante.Ponto.DataHora:yyyy}";
        comprovante.CreationDate ??= transaction.DateTime;
    }
}
