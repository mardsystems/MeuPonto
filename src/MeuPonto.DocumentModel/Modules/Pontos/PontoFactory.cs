using MeuPonto.Modules.Trabalhadores;

namespace MeuPonto.Modules.Pontos;

public static class PontoFactory
{
    public static Ponto CriaPonto(this Trabalhador trabalhador, TransactionContext transaction, Guid? id = null)
    {
        var ponto = new Ponto
        {
            Id = id ?? Guid.NewGuid(),
            TrabalhadorId = trabalhador.Id,
            PartitionKey = $"{trabalhador.Id}",
            CreationDate = transaction.DateTime
        };

        return ponto;
    }

    public static void RecontextualizaPonto(this Trabalhador trabalhador, Ponto ponto, TransactionContext transaction, Guid? id = null)
    {
        ponto.Id = ponto.Id ?? id ?? Guid.NewGuid();
        ponto.TrabalhadorId = trabalhador.Id;
        ponto.PartitionKey = $"{trabalhador.Id}|{ponto.DataHora:yyyy}";
        ponto.CreationDate = transaction.DateTime;
    }
}
