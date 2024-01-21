using System.Transactions;
using Timesheet.Models.Pontos;

namespace Timesheet.Features.BackupComprovantes;

public static class BackupComprovantesFacade
{
    public static Comprovante CriaComprovante(TransactionContext transaction, Guid? id = null)
    {
        var comprovante = new Comprovante
        {
            Id = id ?? Guid.NewGuid(),
            UserId = transaction.UserId,
            CreationDate = transaction.DateTime
        };

        return comprovante;
    }

    public static void RecontextualizaComprovante(this Comprovante comprovante, TransactionContext transaction, Guid? id = null)
    {
        comprovante.Id = comprovante.Id ?? id ?? Guid.NewGuid();
        comprovante.UserId = transaction.UserId;
        comprovante.CreationDate = transaction.DateTime;
    }

    public static void ComprovaPonto(this Comprovante comprovante, Ponto ponto)
    {
        comprovante.Ponto = ponto;

        comprovante.PontoId = ponto.Id;
    }
}
