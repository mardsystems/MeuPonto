using System.Transactions;
using Timesheet.Models.Contratos;
using Timesheet.Models.Pontos;

namespace Timesheet.Features.RegistroPontos;

public static class RegistroPontosFacade
{
    public static Ponto CriaPonto(TransactionContext transaction, Guid? id = null)
    {
        var ponto = new Ponto
        {
            Id = id ?? Guid.NewGuid(),
            UserId = transaction.UserId,
            CreationDate = transaction.DateTime
        };

        return ponto;
    }

    public static void QualificaPonto(this Contrato contrato, Ponto ponto)
    {
        ponto.Contrato = contrato;

        ponto.ContratoId = contrato.Id;
    }

    public static void DesqualificaPonto(this Ponto ponto)
    {
        ponto.Contrato = null;

        ponto.ContratoId = null;
    }

    public static void RecontextualizaPonto(this Ponto ponto, TransactionContext transaction, Guid? id = null)
    {
        ponto.Id = ponto.Id ?? id ?? Guid.NewGuid();
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
