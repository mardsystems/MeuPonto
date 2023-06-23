using MeuPonto.Models;

namespace MeuPonto.Factories;

public static class EmpregadorFactory
{
    public static Empregador CriaEmpregador(TransactionContext transaction)
    {
        var empregador = new Empregador
        {
            CreationDate = transaction.DateTime
        };

        return empregador;
    }
    
    public static void RecontextualizaEmpregador(this Empregador empregador, TransactionContext transaction)
    {
        empregador.CreationDate = transaction.DateTime;
    }
}
