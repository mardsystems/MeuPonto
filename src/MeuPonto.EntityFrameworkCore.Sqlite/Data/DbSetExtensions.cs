using MeuPonto.Models.Timesheet.Empregadores;
using MeuPonto.Models.Timesheet.Contratos;
using MeuPonto.Models.Timesheet.Pontos;
using Microsoft.EntityFrameworkCore;

namespace MeuPonto.Data;

public static class DbSetExtensions
{
    public static ValueTask<TEntity?> FindByIdAsync<TEntity>(this DbSet<TEntity> dbSet, Guid? id, string partitionKey) where TEntity : class
    {
        return dbSet.FindAsync(id);
    }

    public static ValueTask<Empregador> FindByIdAsync(this DbSet<Empregador> dbSet, Guid? id, Guid userId)
    {
        var partitionKey = $"{userId}";

        return dbSet.FindAsync(id);
    }

    public static ValueTask<Contrato> FindByIdAsync(this DbSet<Contrato> dbSet, Guid? id, Guid userId)
    {
        var partitionKey = $"{userId}";

        return dbSet.FindAsync(id);
    }

    public static ValueTask<Ponto> FindByIdAsync(this DbSet<Ponto> dbSet, Guid? id, Guid userId, int ano)
    {
        var partitionKey = $"{userId}|{ano}";

        return dbSet.FindAsync(id);
    }

    public static string HandleException(this Exception ex)
    {
        if (ex.InnerException is Exception sqlException)
        {
            if (sqlException.Message.Contains("Request size is too large"))
            {
                return "Request size is too large";
            }

            return sqlException.Message;
        }

        return ex.Message;
    }
}
