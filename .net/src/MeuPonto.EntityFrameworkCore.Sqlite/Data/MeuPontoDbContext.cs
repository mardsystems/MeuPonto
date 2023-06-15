using MeuPonto.Modules;
using MeuPonto.Modules.Perfis;
using MeuPonto.Modules.Perfis.Empresas;
using MeuPonto.Modules.Pontos;
using MeuPonto.Modules.Pontos.Comprovantes;
using MeuPonto.Modules.Pontos.Folhas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MeuPonto.Data;

public class MeuPontoDbContext : DbContext
{
    public MeuPontoDbContext()
    {

    }

    public MeuPontoDbContext(DbContextOptions<MeuPontoDbContext> options)
        : base(options)
    {

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var dataSource = "C:\\github\\MeuPonto\\.net\\src\\MeuPonto.Desktop\\bin\\Debug\\net6.0-windows\\MeuPonto.db";

        optionsBuilder.UseSqlite($"Data Source={dataSource}");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Empresa>()
            .ToTable("Empresas");

        //modelBuilder.Entity<Empresa>().Property(x => x.Version).IsETagConcurrency();

        modelBuilder.Entity<Perfil>()
            .ToTable("Perfis");

        //modelBuilder.Entity<Perfil>().Property(x => x.Version).IsETagConcurrency();

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

        //modelBuilder.Entity<Folha>().Property(x => x.Version).IsETagConcurrency();

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

        modelBuilder.Entity<Ponto>().Property(x => x.PausaId).HasConversion(new EnumToStringConverter<PausaEnum>());

        //modelBuilder.Entity<Ponto>().Property(x => x.Version).IsETagConcurrency();

        modelBuilder.Entity<Comprovante>()
            .ToTable("Comprovantes");

        //modelBuilder.Entity<Comprovante>().Property(x => x.Version).IsETagConcurrency();

        modelBuilder.Entity<Trabalhador>()
            .ToTable("Trabalhadores")
            .HasNoKey();

        modelBuilder.Entity<ConfiguracaoPorUsuario>()
            .ToTable("Configuracoes")
            .HasNoKey();
    }

    public DbSet<Empresa> Empresas { get; set; }
    public DbSet<Perfil> Perfis { get; set; }
    public DbSet<Folha> Folhas { get; set; }
    public DbSet<Ponto> Pontos { get; set; }
    public DbSet<Comprovante> Comprovantes { get; set; }
    public DbSet<Trabalhador> Trabalhadores { get; set; }
    public DbSet<ConfiguracaoPorUsuario> Configuracoes { get; set; }
}
