using MeuPonto.Models;
using System.Transactions;

namespace MeuPonto.Features;

public static class RegistroPontosFacade
{
    public static Ponto CriaPonto(TransactionContext transaction)
    {
        var ponto = new Ponto
        {
            UserId = transaction.UserId,
            CreationDate = transaction.DateTime
        };

        return ponto;
    }

    public static void RecontextualizaPonto(this Ponto ponto, TransactionContext transaction)
    {
        ponto.UserId = transaction.UserId;
        ponto.CreationDate = transaction.DateTime;
    }

    public static bool EstaQualificado(this Ponto ponto)
    {
        return ponto.ContratoId.HasValue;
    }

    public static bool EstaSemQualificacao(this Ponto ponto)
    {
        return ponto.ContratoId == null;
    }
}
