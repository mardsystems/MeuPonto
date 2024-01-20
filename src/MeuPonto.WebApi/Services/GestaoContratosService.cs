using MeuPonto.Models;
using System.Transactions;

namespace MeuPonto.Services;

public static class GestaoContratosService
{
    public static Contrato InciarAberturaContrato(TransactionContext transaction)
    {
        var contrato = new Contrato
        {
            UserId = transaction.UserId,
            CreationDate = transaction.DateTime
        };

        return contrato;
    }

    public static void RecontextualizaContrato(this Contrato contrato, TransactionContext transaction, Guid? id = null)
    {
        contrato.UserId = transaction.UserId;
        contrato.CreationDate = transaction.DateTime;
    }

    public static Contrato AbrirContrato(this Contrato contrato, Empregador empregador)
    {
        contrato.Empregador = empregador;

        contrato.EmpregadorId = empregador?.Id;

        return contrato;
    }

    public static Contrato AlterarContrato(this Contrato contrato, Empregador empregador)
    {
        contrato.Empregador = empregador;

        contrato.EmpregadorId = empregador?.Id;

        return contrato;
    }

    public static void QualificaPonto(this Contrato contrato, Ponto ponto)
    {
        ponto.Contrato = contrato;

        ponto.ContratoId = contrato.Id;
    }

    public static void QualificaFolha(this Contrato contrato, Folha folha)
    {
        folha.Contrato = contrato;

        folha.ContratoId = contrato.Id;
    }
}
