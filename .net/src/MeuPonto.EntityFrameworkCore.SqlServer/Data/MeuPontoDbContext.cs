using MeuPonto.Enums;
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

        modelBuilder.Entity<Empregador>()
            .ToTable("Empregadores");

        modelBuilder.Entity<Perfil>()
            .ToTable("Perfis");

        modelBuilder.Entity<Perfil>().Property(b => b.CreationDate).HasDefaultValueSql("getdate()");

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
            });
        });

        modelBuilder.Entity<Ponto>()
            .ToTable("Pontos");

        modelBuilder.Entity<Ponto>().Property(b => b.CreationDate).HasDefaultValueSql("getdate()");

        modelBuilder.Entity<Comprovante>()
            .ToTable("Comprovantes");

        modelBuilder.Entity<Comprovante>().Property(b => b.CreationDate).HasDefaultValueSql("getdate()");

        modelBuilder.Entity<TipoImagem>().Property(x => x.Id).ValueGeneratedNever();

        modelBuilder.Entity<TipoImagem>().HasData(
            new TipoImagem { Id = TipoImagemEnum.Original, Nome = "Original" },
            new TipoImagem { Id = TipoImagemEnum.Tratada, Nome = "Tratada" }
        );

        modelBuilder.Entity<Trabalhador>()
            .ToTable("Trabalhadores")
            .HasNoKey();

        modelBuilder.Entity<ConfiguracaoPorUsuario>()
            .ToTable("Configuracoes")
            .HasNoKey();
    }

    public DbSet<Empregador> Empregadores { get; set; }
    public DbSet<Perfil> Perfis { get; set; }
    public DbSet<Folha> Folhas { get; set; }
    public DbSet<Ponto> Pontos { get; set; }
    public DbSet<Comprovante> Comprovantes { get; set; }
    public DbSet<Trabalhador> Trabalhadores { get; set; }
    public DbSet<ConfiguracaoPorUsuario> Configuracoes { get; set; }
}
