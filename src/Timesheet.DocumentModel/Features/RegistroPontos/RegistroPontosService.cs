using System.Transactions;
using Timesheet.Models.Contratos;
using Timesheet.Models.Pontos;

namespace Timesheet.Features.RegistroPontos;

public static class RegistroPontosService
{
    public static Ponto CriaPonto(TransactionContext transaction, Guid? id = null)
    {
        var ponto = new Ponto
        {
            Id = id ?? Guid.NewGuid(),
            UserId = transaction.UserId,
            PartitionKey = $"{transaction.UserId}",
            CreationDate = transaction.DateTime
        };

        return ponto;
    }

    public static void RecontextualizaPonto(this Ponto ponto, TransactionContext transaction, Guid? id = null)
    {
        ponto.Id ??= id ?? Guid.NewGuid();
        ponto.UserId = transaction.UserId;
        ponto.PartitionKey = $"{transaction.UserId}|{ponto.DataHora:yyyy}";
        ponto.CreationDate ??= transaction.DateTime;
    }

    public static void QualificaPonto(this Contrato contrato, Ponto ponto)
    {
        ponto.Contrato = new ContratoRef
        {
            Nome = contrato.Nome
        };

        ponto.ContratoId = contrato.Id;
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
