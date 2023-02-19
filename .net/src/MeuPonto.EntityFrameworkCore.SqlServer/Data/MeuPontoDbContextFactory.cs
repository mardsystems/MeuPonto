using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeuPonto.Data
{
    internal class MeuPontoDbContextFactory : IDesignTimeDbContextFactory<MeuPontoDbContext>
    {
        public MeuPontoDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MeuPontoDbContext>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=MeuPonto-Table;Trusted_Connection=True;MultipleActiveResultSets=true");

            return new MeuPontoDbContext(optionsBuilder.Options);
        }
    }
}
