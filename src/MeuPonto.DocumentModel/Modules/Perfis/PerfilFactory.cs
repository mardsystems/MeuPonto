using MeuPonto.Modules.Trabalhadores;

namespace MeuPonto.Modules.Perfis;

public static class PerfilFactory
{
    public static Perfil CriaPerfil(this Trabalhador trabalhador, TransactionContext transaction, Guid? id = null)
    {
        var perfil = new Perfil
        {
            Id = id ?? Guid.NewGuid(),
            TrabalhadorId = trabalhador.Id,
            PartitionKey = trabalhador.Id.ToString(),
            CreationDate = transaction.DateTime
        };

        return perfil;
    }

    public static void RecontextualizaPerfil(this Trabalhador trabalhador, Perfil perfil, TransactionContext transaction, Guid? id = null)
    {
        perfil.Id = perfil.Id ?? id ?? Guid.NewGuid();
        perfil.TrabalhadorId = trabalhador.Id;
        perfil.PartitionKey = trabalhador.Id.ToString();
        perfil.CreationDate = transaction.DateTime;
    }
}
