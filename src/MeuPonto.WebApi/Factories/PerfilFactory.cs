using MeuPonto.Models;

namespace MeuPonto.Factories;

public static class PerfilFactory
{
    public static Perfil CriaPerfil(TransactionContext transaction)
    {
        var perfil = new Perfil
        {
            UserId = transaction.UserId,
            CreationDate = transaction.DateTime
        };

        return perfil;
    }
    
    public static void RecontextualizaPerfil(this Perfil perfil, TransactionContext transaction, Guid? id = null)
    {
        perfil.UserId = transaction.UserId;
        perfil.CreationDate = transaction.DateTime;
    }
}
