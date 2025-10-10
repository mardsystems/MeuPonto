using MeuPonto.Models;
using Microsoft.EntityFrameworkCore;

namespace MeuPonto.Data;

public class MeuPontoDbContext : DbContext
{
    public MeuPontoDbContext(DbContextOptions<MeuPontoDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Contrato>()
            .ToTable("Contratos");

        modelBuilder.Entity<Contrato>().Property(b => b.CreationDate).HasDefaultValueSql("getdate()");

        modelBuilder.Entity<Contrato>().OwnsOne(a => a.JornadaTrabalhoSemanalPrevista, x =>
        {
            x.OwnsMany(b => b.Semana, y =>
            {
                y.ToTable("Contratos_JornadaTrabalhoDiaria");
                y.WithOwner().HasForeignKey("ContratoId");
                y.HasKey("ContratoId", "DiaSemana");
            });
        });
    }
    public DbSet<Contrato> Contratos { get; set; }
}
