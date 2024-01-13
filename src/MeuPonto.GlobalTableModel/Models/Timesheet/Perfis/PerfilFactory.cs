namespace MeuPonto.Models.Timesheet.Perfis;

public static class PerfilFactory
{
    public static Perfil CriaPerfil(TransactionContext transaction, Guid? id = null)
    {
        var perfil = new Perfil
        {
            Id = id ?? Guid.NewGuid(),
            UserId = transaction.UserId,
            CreationDate = transaction.DateTime
        };

        return perfil;
    }

    public static void RecontextualizaPerfil(this Perfil perfil, TransactionContext transaction, Guid? id = null)
    {
        perfil.Id = perfil.Id ?? id ?? Guid.NewGuid();
        perfil.UserId = transaction.UserId;
        perfil.CreationDate = transaction.DateTime;
    }
}
