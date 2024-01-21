using MeuPonto.Models;
using Timesheet.Models.Contratos;
using Timesheet.Models.Pontos;
using Microsoft.EntityFrameworkCore;
using Timesheet.Models.Folhas;
using MeuPonto.Models.Trabalhadores;

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

        modelBuilder.Entity<Configuracoes>()
            .ToTable("Configuracoes")
            .HasNoKey();

        modelBuilder.Entity<Trabalhador>()
            .ToTable("Trabalhadores");

        modelBuilder.Entity<Trabalhador>().Property(b => b.CreationDate).HasDefaultValueSql("getdate()");

        modelBuilder.Entity<Empregador>()
            .ToTable("Empregadores");

        modelBuilder.Entity<Empregador>().Property(b => b.CreationDate).HasDefaultValueSql("getdate()");

        modelBuilder.Entity<Contrato>()
            .ToTable("Contratos");

        modelBuilder.Entity<Contrato>().Property(b => b.CreationDate).HasDefaultValueSql("getdate()");

        modelBuilder.Entity<Contrato>().HasOne(a => a.Empregador).WithMany().HasForeignKey(a => a.EmpregadorId).OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Contrato>().OwnsOne(a => a.JornadaTrabalhoSemanalPrevista, x =>
        {
            x.OwnsMany(b => b.Semana, y =>
            {
                y.ToTable("Contratos_JornadaTrabalhoDiaria");
                y.WithOwner().HasForeignKey("ContratoId");
                y.HasKey("ContratoId", "DiaSemana");
            });
        });

        modelBuilder.Entity<Ponto>()
            .ToTable("Pontos");

        modelBuilder.Entity<Ponto>().Property(b => b.CreationDate).HasDefaultValueSql("getdate()");

        modelBuilder.Entity<Ponto>().HasOne(a => a.Contrato).WithMany().HasForeignKey(a => a.ContratoId).OnDelete(DeleteBehavior.Restrict);

        //modelBuilder.Entity<Ponto>().Property(x => x.PausaId).HasConversion(new EnumToStringConverter<PausaEnum>());

        modelBuilder.Entity<Momento>().Property(x => x.Id).ValueGeneratedNever();

        modelBuilder.Entity<Momento>().HasData(
            new Momento { Id = MomentoEnum.Entrada, Nome = "Entrada" },
            new Momento { Id = MomentoEnum.Saida, Nome = "Saída" },
            new Momento { Id = MomentoEnum.Errado, Nome = "Errado" }
        );

        modelBuilder.Entity<Pausa>().Property(x => x.Id).ValueGeneratedNever();

        modelBuilder.Entity<Pausa>().HasData(
            new Pausa { Id = PausaEnum.Almoco, Nome = "Almoço" },
            new Pausa { Id = PausaEnum.CafeLanche, Nome = "Café/Lanche" },
            new Pausa { Id = PausaEnum.Banheiro, Nome = "Banheiro" },
            new Pausa { Id = PausaEnum.ConversaReuniao, Nome = "Conversa/Reunião" },
            new Pausa { Id = PausaEnum.Telefonema, Nome = "Telefonema" },
            new Pausa { Id = PausaEnum.Generica, Nome = "Genérica" }
        );

        modelBuilder.Entity<Comprovante>()
            .ToTable("Comprovantes");
        
        modelBuilder.Entity<Comprovante>().Property(b => b.CreationDate).HasDefaultValueSql("getdate()");

        modelBuilder.Entity<Comprovante>().HasOne(a => a.Ponto).WithMany(b => b.Comprovantes).HasForeignKey(a => a.PontoId).OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<TipoImagem>().Property(x => x.Id).ValueGeneratedNever();

        modelBuilder.Entity<TipoImagem>().HasData(
            new TipoImagem { Id = TipoImagemEnum.Original, Nome = "Original" },
            new TipoImagem { Id = TipoImagemEnum.Tratada, Nome = "Tratada" }
        );

        modelBuilder.Entity<Folha>()
            .ToTable("Folhas");

        modelBuilder.Entity<Folha>().Property(b => b.CreationDate).HasDefaultValueSql("getdate()");

        modelBuilder.Entity<Folha>().HasOne(a => a.Contrato).WithMany().HasForeignKey(a => a.ContratoId).OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Folha>().OwnsOne(a => a.ApuracaoMensal, x =>
        {
            x.OwnsMany(b => b.Dias, y =>
            {
                y.ToTable("Folhas_ApuracaoDiaria");
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

        modelBuilder.Entity<StatusFolha>().Property(x => x.Id).ValueGeneratedNever();

        modelBuilder.Entity<StatusFolha>().HasData(
            new StatusFolha { Id = StatusFolhaEnum.Aberta, Nome = "Aberta" },
            new StatusFolha { Id = StatusFolhaEnum.Fechada, Nome = "Fechada" }
        );
    }

    public DbSet<Configuracoes> Configuracoes { get; set; }
    public DbSet<Trabalhador> Trabalhadores { get; set; }
    public DbSet<Empregador> Empregadores { get; set; }
    public DbSet<Contrato> Contratos { get; set; }
    public DbSet<Ponto> Pontos { get; set; }
    public DbSet<Comprovante> Comprovantes { get; set; }
    public DbSet<Folha> Folhas { get; set; }
}
