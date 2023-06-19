namespace MeuPonto.Modules.Perfis;

public static class PerfilFactory
{
    public static Perfil CriaPerfil(TransactionContext transaction, Guid? id = null)
    {
        var ponto = new Perfil
        {
            Id = id ?? Guid.NewGuid(),
            PartitionKey = transaction.UserName,
            CreationDate = transaction.DateTime
        };

        return ponto;
    }
}
