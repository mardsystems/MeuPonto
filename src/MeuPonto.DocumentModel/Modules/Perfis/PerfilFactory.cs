namespace MeuPonto.Modules.Perfis;

public static class PerfilFactory
{
    public static Perfil CriaPerfil(TransactionContext transaction, Guid? id = null)
    {
        var perfil = new Perfil
        {
            Id = id ?? Guid.NewGuid(),
            PartitionKey = transaction.UserName,
            CreationDate = transaction.DateTime
        };

        return perfil;
    }

    public static void RecontextualizaPerfil(this Perfil perfil, TransactionContext transaction, Guid? id = null)
    {
        perfil.Id = perfil.Id ?? id ?? Guid.NewGuid();
        perfil.PartitionKey = transaction.UserName;
        perfil.CreationDate = transaction.DateTime;
    }
}
