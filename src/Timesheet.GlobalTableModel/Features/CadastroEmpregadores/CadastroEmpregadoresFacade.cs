﻿using System.Transactions;
using Timesheet.Models.Contratos;

namespace Timesheet.Features.CadastroEmpregadores;

public static class CadastroEmpregadoresFacade
{
    public static Empregador CriaEmpregador(TransactionContext transaction, Guid? id = null)
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
}
