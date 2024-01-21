using System.Transactions;
using Timesheet.Models.Contratos;

namespace Timesheet.Features.GestaoContratos;

public static class GestaoContratosService
{
    public static Contrato InciarAberturaContrato(TransactionContext transaction, Guid? id = null)
    {
        var contrato = new Contrato
        {
            Id = id ?? Guid.NewGuid(),
            UserId = transaction.UserId,
            CreationDate = transaction.DateTime
        };

        return contrato;
    }

    public static void RecontextualizaContrato(this Contrato contrato, TransactionContext transaction, Guid? id = null)
    {
        contrato.Id = contrato.Id ?? id ?? Guid.NewGuid();
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
}
