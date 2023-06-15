﻿using MeuPonto.Enums;
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

        modelBuilder.Entity<TipoImagem>().Property(x => x.Id).ValueGeneratedNever();

        modelBuilder.Entity<TipoImagem>().HasData(
            new TipoImagem { Id = TipoImagemEnum.Original, Nome = "Original" },
            new TipoImagem { Id = TipoImagemEnum.Tratada, Nome = "Tratada" }
        );
    }

    public DbSet<Perfil> Perfis { get; set; }
    public DbSet<Ponto> Pontos { get; set; }
    public DbSet<Comprovante> Comprovantes { get; set; }
    public DbSet<TipoImagem> PontoComprovanteImagemTipos { get; set; }
}
