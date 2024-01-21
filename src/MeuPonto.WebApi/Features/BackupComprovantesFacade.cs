using MeuPonto.Models;
using System.Transactions;

namespace MeuPonto.Features;

public static class BackupComprovantesFacade
{
    public static Comprovante CriaComprovante(TransactionContext transaction)
    {
        var comprovante = new Comprovante
        {
            UserId = transaction.UserId,
            CreationDate = transaction.DateTime
        };

        return comprovante;
    }

    public static void RecontextualizaComprovante(this Comprovante comprovante, TransactionContext transaction)
    {
        comprovante.UserId = transaction.UserId;
        comprovante.CreationDate = transaction.DateTime;
    }

    public static void ComprovaPonto(this Comprovante comprovante, Ponto ponto)
    {
        comprovante.Ponto = new Ponto
        {
            ContratoId = ponto?.ContratoId,
            DataHora = ponto?.DataHora,
            Contrato = ponto?.Contrato,
            MomentoId = ponto?.MomentoId,
            PausaId = ponto?.PausaId
        };

        comprovante.PontoId = ponto.Id;
    }
}
