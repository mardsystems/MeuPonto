using MeuPonto.Models;

namespace MeuPonto.Factories;

public static class TrabalhadorFactory
{
    public static Trabalhador CriaTrabalhador(TransactionContext transaction)
    {
        var trabalhador = new Trabalhador
        {
            CreationDate = transaction.DateTime
        };

        return trabalhador;
    }

    public static void RecontextualizaTrabalhador(this Trabalhador trabalhador, TransactionContext transaction)
    {
        trabalhador.CreationDate ??= transaction.DateTime;
    }
}
