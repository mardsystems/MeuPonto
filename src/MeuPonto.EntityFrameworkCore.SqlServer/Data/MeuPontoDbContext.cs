﻿using MeuPonto.Modules;
using MeuPonto.Modules.Empregadores;
using MeuPonto.Modules.Perfis;
using MeuPonto.Modules.Pontos;
using MeuPonto.Modules.Pontos.Comprovantes;
using MeuPonto.Modules.Pontos.Folhas;
using MeuPonto.Modules.Trabalhadores;
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

        modelBuilder.Entity<Empregador>()
            .ToTable("Empregadores");

        modelBuilder.Entity<Perfil>()
            .ToTable("Perfis");

        modelBuilder.Entity<Perfil>().OwnsOne(a => a.JornadaTrabalhoSemanalPrevista, x =>
        {
            x.OwnsMany(b => b.Semana, y =>
            {
                y.WithOwner().HasForeignKey("PerfilId");
                y.HasKey("PerfilId", "DiaSemana");
            });
        });

        modelBuilder.Entity<Folha>()
            .ToTable("Folhas");

        modelBuilder.Entity<Folha>().OwnsOne(a => a.ApuracaoMensal, x =>
        {
            x.OwnsMany(b => b.Dias, y =>
            {
                y.WithOwner().HasForeignKey("FolhaId");
                y.HasKey("FolhaId", "Dia");

                y.Property(x => x.Dia).ValueGeneratedNever();

                y.Property(x => x.TempoPrevisto).HasConversion<long>();
                y.Property(x => x.TempoApurado).HasConversion<long>();
                y.Property(x => x.DiferencaTempo).HasConversion<long>();
                y.Property(x => x.TempoAbonado).HasConversion<long>();
            });

            x.Property(x => x.TempoTotalPeriodoAnterior).HasConversion<long>();
        });

        modelBuilder.Entity<Ponto>()
            .ToTable("Pontos");

        modelBuilder.Entity<Comprovante>()
            .ToTable("Comprovantes");

        modelBuilder.Entity<Trabalhador>()
            .ToTable("Trabalhadores");

        modelBuilder.Entity<Configuracoes>()
            .ToTable("Configuracoes")
            .HasNoKey();

        //

        modelBuilder.Entity<Trabalhador>().Property(b => b.CreationDate).HasDefaultValueSql("getdate()");
        modelBuilder.Entity<Empregador>().Property(b => b.CreationDate).HasDefaultValueSql("getdate()");
        modelBuilder.Entity<Perfil>().Property(b => b.CreationDate).HasDefaultValueSql("getdate()");
        modelBuilder.Entity<Ponto>().Property(b => b.CreationDate).HasDefaultValueSql("getdate()");
        modelBuilder.Entity<Comprovante>().Property(b => b.CreationDate).HasDefaultValueSql("getdate()");
        modelBuilder.Entity<Folha>().Property(b => b.CreationDate).HasDefaultValueSql("getdate()");

        //

        modelBuilder.Entity<Status>().Property(x => x.Id).ValueGeneratedNever();
        modelBuilder.Entity<Momento>().Property(x => x.Id).ValueGeneratedNever();
        modelBuilder.Entity<Pausa>().Property(x => x.Id).ValueGeneratedNever();
        modelBuilder.Entity<TipoImagem>().Property(x => x.Id).ValueGeneratedNever();

        //

        modelBuilder.Entity<Status>().HasData(
            new Status { Id = StatusEnum.Aberta, Nome = "Aberta" },
            new Status { Id = StatusEnum.Fechada, Nome = "Fechada" }
        );

        modelBuilder.Entity<Momento>().HasData(
            new Momento { Id = MomentoEnum.Entrada, Nome = "Entrada" },
            new Momento { Id = MomentoEnum.Saida, Nome = "Saída" },
            new Momento { Id = MomentoEnum.Errado, Nome = "Errado" }
        );

        modelBuilder.Entity<Pausa>().HasData(
            new Pausa { Id = PausaEnum.Almoco, Nome = "Almoço" },
            new Pausa { Id = PausaEnum.CafeLanche, Nome = "Café/Lanche" },
            new Pausa { Id = PausaEnum.Banheiro, Nome = "Banheiro" },
            new Pausa { Id = PausaEnum.ConversaReuniao, Nome = "Conversa/Reunião" },
            new Pausa { Id = PausaEnum.Telefonema, Nome = "Telefonema" },
            new Pausa { Id = PausaEnum.Generica, Nome = "Genérica" }
        );

        modelBuilder.Entity<TipoImagem>().HasData(
            new TipoImagem { Id = TipoImagemEnum.Original, Nome = "Original" },
            new TipoImagem { Id = TipoImagemEnum.Tratada, Nome = "Tratada" }
        );

        //

        //modelBuilder.Entity<Ponto>().Property(x => x.PausaId).HasConversion(new EnumToStringConverter<PausaEnum>());
    }

    public DbSet<Empregador> Empregadores { get; set; }
    public DbSet<Perfil> Perfis { get; set; }
    public DbSet<Folha> Folhas { get; set; }
    public DbSet<Ponto> Pontos { get; set; }
    public DbSet<Comprovante> Comprovantes { get; set; }
    public DbSet<Trabalhador> Trabalhadores { get; set; }
    public DbSet<Configuracoes> Configuracoes { get; set; }
}
