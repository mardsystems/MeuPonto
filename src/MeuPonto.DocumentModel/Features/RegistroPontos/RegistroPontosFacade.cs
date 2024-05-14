using System.Transactions;
using MeuPonto.Models.Contratos;
using MeuPonto.Models.Pontos;

namespace MeuPonto.Features.RegistroPontos;

public static class RegistroPontosFacade
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
        if (contrato == null)
        {
            return;
        }

        ponto.Contrato = new ContratoRef
        {
            Nome = contrato.Nome
        };

        ponto.ContratoId = contrato.Id;
    }

    public static void DesqualificaPonto(this Ponto ponto)
    {
        ponto.Contrato = null;

        ponto.ContratoId = null;
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
