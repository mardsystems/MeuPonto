using MeuPonto.Modules.Perfis;
using MeuPonto.Modules.Pontos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeuPonto.Data;

public class MeuPontoDbContext : DbContext
{
    public MeuPontoDbContext(DbContextOptions<MeuPontoDbContext> options)
        : base(options)
    {
        SavingChanges += MeuPontoDbContext_SavingChanges;
    }

    private void MeuPontoDbContext_SavingChanges(object? sender, SavingChangesEventArgs e)
    {
        var perfilsAlterados = ChangeTracker
            .Entries<Perfil>()
            .Where(x => x.State == EntityState.Modified)
            .Select(x => x.Entity);

        foreach (var perfilAlterado in perfilsAlterados)
        {
            var pontos = Pontos.Where(x => x.PerfilId == perfilAlterado.Id);

            foreach (var ponto in pontos)
            {
                ponto.Perfil.Matricula = perfilAlterado.Matricula;
            }
        }

        var pontosAlterados = ChangeTracker
            .Entries<Ponto>()
            .Where(x => x.State == EntityState.Modified)
            .Select(x => x.Entity);

        foreach (var pontoAlterado in pontosAlterados)
        {
            var pontoComprovantes = PontoComprovantes.Where(x => x.PontoId == pontoAlterado.Id);

            foreach (var pontoComprovante in pontoComprovantes)
            {
                pontoComprovante.Ponto.Data = pontoAlterado.Data;
            }
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Perfil>()
            .ToContainer("Perfis")
            .HasNoDiscriminator()
            .HasPartitionKey(x => x.PartitionKey);

        modelBuilder.Entity<Perfil>().Property(x => x.Version).IsETagConcurrency();

        modelBuilder.Entity<Ponto>()
            .ToContainer("Pontos")
            .HasNoDiscriminator()
            .HasPartitionKey(x => x.PartitionKey);

        modelBuilder.Entity<Ponto>().Property(x => x.Version).IsETagConcurrency();

        modelBuilder.Entity<PontoComprovante>()
            .ToContainer("PontoComprovantes")
            .HasNoDiscriminator()
            .HasPartitionKey(x => x.PartitionKey);

        modelBuilder.Entity<PontoComprovante>().Property(x => x.Version).IsETagConcurrency();

        modelBuilder.Entity<PontoComprovanteImagemTipo>().
            ToContainer("PontoComprovanteImagemTipos")
            .HasNoDiscriminator();

        modelBuilder.Entity<PontoComprovanteImagemTipo>().HasData(
            new PontoComprovanteImagemTipo { Id = (int)PontoComprovanteImagemTipoEnum.Original, Nome = "Original" },
            new PontoComprovanteImagemTipo { Id = (int)PontoComprovanteImagemTipoEnum.Tratada, Nome = "Tratada" }
        );
    }

    public DbSet<Perfil> Perfis { get; set; }
    public DbSet<Ponto> Pontos { get; set; }
    public DbSet<PontoComprovante> PontoComprovantes { get; set; }
    public DbSet<PontoComprovanteImagemTipo> PontoComprovanteImagemTipos { get; set; }
}
