using MeuPonto.Models;
using System.Transactions;

namespace MeuPonto.Services;

public static class CadastroEmpregadoresService
{
    public static Empregador CriaEmpregador(TransactionContext transaction)
    {
        var empregador = new Empregador
        {
            UserId = transaction.UserId,
            CreationDate = transaction.DateTime
        };

        return empregador;
    }

    public static void RecontextualizaEmpregador(this Empregador empregador, TransactionContext transaction)
    {
        empregador.UserId = transaction.UserId;
        empregador.CreationDate = transaction.DateTime;
    }
}
