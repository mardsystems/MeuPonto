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

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Perfil>().ToContainer("Perfis");
        modelBuilder.Entity<Perfil>().HasNoDiscriminator();
        modelBuilder.Entity<Perfil>().Property(p => p.ETag).IsETagConcurrency();
        modelBuilder.Entity<Ponto>().ToContainer("Pontos");
        modelBuilder.Entity<Ponto>().HasNoDiscriminator();
        modelBuilder.Entity<Ponto>().Property(p => p.ETag).IsETagConcurrency();
        modelBuilder.Entity<PontoComprovante>().ToContainer("PontoComprovantes");
        modelBuilder.Entity<PontoComprovante>().HasNoDiscriminator();
        modelBuilder.Entity<PontoComprovante>().Property(p => p.ETag).IsETagConcurrency();
        modelBuilder.Entity<PontoComprovanteImagemTipo>().ToContainer("PontoComprovanteImagemTipos");
        modelBuilder.Entity<PontoComprovanteImagemTipo>().HasNoDiscriminator();

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
