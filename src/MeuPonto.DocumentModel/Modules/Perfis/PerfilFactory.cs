namespace MeuPonto.Modules.Perfis;

public static class PerfilFactory
{
    public static Perfil CriaPerfil(TransactionContext transaction, Guid? id = null)
    {
        var perfil = new Perfil
        {
            Id = id ?? Guid.NewGuid(),
            TrabalhadorId = transaction.UserId,
            PartitionKey = transaction.UserId.ToString(),
            CreationDate = transaction.DateTime
        };

        return perfil;
    }

    public static void RecontextualizaPerfil(this Perfil perfil, TransactionContext transaction, Guid? id = null)
    {
        perfil.Id ??= id ?? Guid.NewGuid();
        perfil.TrabalhadorId = transaction.UserId;
        perfil.PartitionKey = transaction.UserId.ToString();
        perfil.CreationDate ??= transaction.DateTime;
    }
}
