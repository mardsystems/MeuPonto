using MeuPonto.Modules.Empregadores;
using MeuPonto.Modules.Perfis;
using MeuPonto.Modules.Pontos;
using MeuPonto.Modules.Trabalhadores;
using Microsoft.EntityFrameworkCore;

namespace MeuPonto.Data;

public static class DbSetExtensions
{
    public static ValueTask<TEntity?> FindByIdAsync<TEntity>(this DbSet<TEntity> dbSet, Guid? id, string partitionKey) where TEntity : class
    {
        return dbSet.FindAsync(id, partitionKey);
    }

    public static ValueTask<Empregador> FindByIdAsync(this DbSet<Empregador> dbSet, Guid? id, Trabalhador trabalhador)
    {
        var partitionKey = $"{trabalhador.Id}";

        return dbSet.FindAsync(id, partitionKey);
    }

    public static ValueTask<Perfil> FindByIdAsync(this DbSet<Perfil> dbSet, Guid? id, Trabalhador trabalhador)
    {
        var partitionKey = $"{trabalhador.Id}";

        return dbSet.FindAsync(id, partitionKey);
    }

    public static ValueTask<Ponto> FindByIdAsync(this DbSet<Ponto> dbSet, Guid? id, Trabalhador trabalhador, int ano)
    {
        var partitionKey = $"{trabalhador.Id}|{ano}";

        return dbSet.FindAsync(id, partitionKey);
    }
}
