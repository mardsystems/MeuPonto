using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MeuPonto.Data;

internal class MeuPontoDbContextFactory : IDesignTimeDbContextFactory<MeuPontoDbContext>
{
    public MeuPontoDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<MeuPontoDbContext>();
        optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=MeuPonto-Table;Trusted_Connection=True;MultipleActiveResultSets=true");

        return new MeuPontoDbContext(optionsBuilder.Options);
    }
}
