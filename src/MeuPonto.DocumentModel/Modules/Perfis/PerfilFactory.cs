namespace MeuPonto.Modules.Perfis;

public static class PerfilFactory
{
    public static Perfil CriaPerfil(TransactionContext transaction, Guid? id = null)
    {
        var perfil = new Perfil
        {
            Id = id ?? Guid.NewGuid(),
            PartitionKey = transaction.UserId.ToString(),
            CreationDate = transaction.DateTime
        };

        return perfil;
    }

    public static void RecontextualizaPerfil(this Perfil perfil, TransactionContext transaction, Guid? id = null)
    {
        perfil.Id = perfil.Id ?? id ?? Guid.NewGuid();
        perfil.PartitionKey = transaction.UserId.ToString();
        perfil.CreationDate = transaction.DateTime;
    }
}
