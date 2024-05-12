using System.Transactions;
using MeuPonto.Models.Contratos;

namespace MeuPonto.Features.CadastroEmpregadores;

public static class CadastroEmpregadoresFacade
{
    public static Empregador CriaEmpregador(this TransactionContext transaction, Guid? id = null)
    {
        var empregador = new Empregador
        {
            Id = id ?? Guid.NewGuid(),
            UserId = transaction.UserId,
            CreationDate = transaction.DateTime
        };

        return empregador;
    }

    public static void RecontextualizaEmpregador(this Empregador empregador, TransactionContext transaction, Guid? id = null)
    {
        empregador.Id = empregador.Id ?? id ?? Guid.NewGuid();
        empregador.UserId = transaction.UserId;
        empregador.CreationDate = transaction.DateTime;
    }

    public static void FeitoCom(this Contrato contrato, Empregador empregador)
    {
        contrato.Empregador = empregador;

        contrato.EmpregadorId = empregador?.Id;
    }
}
