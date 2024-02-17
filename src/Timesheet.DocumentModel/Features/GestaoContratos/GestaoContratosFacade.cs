using System.Transactions;
using Timesheet.Models.Contratos;

namespace Timesheet.Features.GestaoContratos;

public static class GestaoContratosFacade
{
    public static Contrato InciarAberturaContrato(TransactionContext transaction, Guid? id = null)
    {
        var contrato = new Contrato
        {
            Id = id ?? Guid.NewGuid(),
            UserId = transaction.UserId,
            PartitionKey = transaction.UserId.ToString(),
            CreationDate = transaction.DateTime
        };

        contrato.Ativo = true;

        return contrato;
    }

    public static void RecontextualizaContrato(this Contrato contrato, TransactionContext transaction, Guid? id = null)
    {
        contrato.Id ??= id ?? Guid.NewGuid();
        contrato.UserId = transaction.UserId;
        contrato.PartitionKey = transaction.UserId.ToString();
        contrato.CreationDate ??= transaction.DateTime;
    }

    public static void FeitoCom(this Contrato contrato, Empregador empregador)
    {
        if (empregador != null)
        {
            contrato.Empregador = new EmpregadorRef
            {
                Nome = empregador.Nome
            };

            contrato.EmpregadorId = empregador.Id;
        }
    }

    public static Contrato AbrirContrato(this Contrato contrato, Empregador empregador)
    {
        if (empregador != null)
        {
            contrato.Empregador = new EmpregadorRef
            {
                Nome = empregador.Nome
            };

            contrato.EmpregadorId = empregador.Id;
        }

        return contrato;
    }

    public static Contrato AlterarContrato(this Contrato contrato, Empregador empregador)
    {
        if (empregador != null)
        {
            contrato.Empregador = new EmpregadorRef
            {
                Nome = empregador.Nome
            };

            contrato.EmpregadorId = empregador.Id;
        }

        return contrato;
    }
}
