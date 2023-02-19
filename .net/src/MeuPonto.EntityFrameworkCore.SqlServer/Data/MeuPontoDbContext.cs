
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

        modelBuilder.Entity<Perfil>().Property(b => b.CreationDate).HasDefaultValueSql("getdate()");
        modelBuilder.Entity<Ponto>().Property(b => b.CreationDate).HasDefaultValueSql("getdate()");
        modelBuilder.Entity<Comprovante>().Property(b => b.CreationDate).HasDefaultValueSql("getdate()");

        modelBuilder.Entity<PontoComprovanteImagemTipo>().Property(x => x.Id).ValueGeneratedNever();

        modelBuilder.Entity<PontoComprovanteImagemTipo>().HasData(
            new PontoComprovanteImagemTipo { Id = (int)Enums.PontoComprovanteImagemTipo.Original, Nome = "Original" },
            new PontoComprovanteImagemTipo { Id = (int)Enums.PontoComprovanteImagemTipo.Tratada, Nome = "Tratada" }
        );
    }

    public DbSet<Perfil> Perfis { get; set; }
    public DbSet<Ponto> Pontos { get; set; }
    public DbSet<Comprovante> Comprovantes { get; set; }
    public DbSet<PontoComprovanteImagemTipo> PontoComprovanteImagemTipos { get; set; }
}
