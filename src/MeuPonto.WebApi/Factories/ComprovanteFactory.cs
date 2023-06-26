using MeuPonto.Models;

namespace MeuPonto.Factories;

public static class ComprovanteFactory
{
    public static Comprovante CriaComprovante(TransactionContext transaction)
    {
        var comprovante = new Comprovante
        {
            CreationDate = transaction.DateTime
        };

        return comprovante;
    }

    public static void RecontextualizaComprovante(this Comprovante comprovante, TransactionContext transaction)
    {
        comprovante.CreationDate = transaction.DateTime;
    }
}
