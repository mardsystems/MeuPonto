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
    public MeuPontoDbContext(DbContextOptions<MeuPontoDbContext> options)
        : base(options)
    {
        SavingChanges += MeuPontoDbContext_SavingChanges;
    }

    private void MeuPontoDbContext_SavingChanges(object? sender, SavingChangesEventArgs e)
    {
        var empresasAlteradas = ChangeTracker
            .Entries<Empresa>()
            .Where(x => x.State == EntityState.Modified)
            .Select(x => x.Entity);

        foreach (var empresaAlterada in empresasAlteradas)
        {
            var perfis = Perfis.Where(x => x.EmpresaId == empresaAlterada.Id);

            foreach (var perfil in perfis)
            {
                perfil.Empresa.Nome = empresaAlterada.Nome;
                perfil.Empresa.Cnpj = empresaAlterada.Cnpj;
            }
        }

        var perfilsAlterados = ChangeTracker
        .Entries<Perfil>()
        .Where(x => x.State == EntityState.Modified)
        .Select(x => x.Entity);

        foreach (var perfilAlterado in perfilsAlterados)
        {
            var pontos = Pontos.Where(x => x.PerfilId == perfilAlterado.Id);

            foreach (var ponto in pontos)
            {
                ponto.Perfil.Nome = perfilAlterado.Nome;
            }

            var folhas = Folhas.Where(x => x.PerfilId == perfilAlterado.Id);

            foreach (var folha in folhas)
            {
                folha.Perfil.Nome = perfilAlterado.Nome;
            }
        }

        var pontosAlterados = ChangeTracker
            .Entries<Ponto>()
            .Where(x => x.State == EntityState.Modified)
            .Select(x => x.Entity);

        foreach (var pontoAlterado in pontosAlterados)
        {
            var comprovantes = Comprovantes.Where(x => x.PontoId == pontoAlterado.Id);

            foreach (var comprovante in comprovantes)
            {
                comprovante.Ponto.PerfilId = pontoAlterado.PerfilId;
                comprovante.Ponto.Perfil = pontoAlterado.Perfil;
                comprovante.Ponto.DataHora = pontoAlterado.DataHora;
                comprovante.Ponto.Momento = pontoAlterado.Momento;
                comprovante.Ponto.Pausa = pontoAlterado.Pausa;
            }
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Empresa>()
            .ToContainer("Perfis")
            .HasPartitionKey(x => x.PartitionKey);

        modelBuilder.Entity<Empresa>().Property(x => x.Version).IsETagConcurrency();

        modelBuilder.Entity<Perfil>()
            .ToContainer("Perfis")
            .HasPartitionKey(x => x.PartitionKey);
        //.HasKey(x => new { x.Id, x.PartitionKey });

        modelBuilder.Entity<Perfil>().Property(x => x.Version).IsETagConcurrency();

        modelBuilder.Entity<Perfil>().OwnsOne(a => a.JornadaTrabalhoSemanalPrevista, x =>
        {
            x.OwnsMany(b => b.Semana, y =>
            {
                //y.HasKey(c => c.DiaSemana);
            });
        });

        modelBuilder.Entity<Folha>()
            .ToContainer("Pontos")
            .HasPartitionKey(x => x.PartitionKey);

        modelBuilder.Entity<Folha>().Property(x => x.Version).IsETagConcurrency();

        modelBuilder.Entity<Folha>().OwnsOne(a => a.ApuracaoMensal, x =>
        {
            x.OwnsMany(b => b.Dias, y =>
            {
                //y.HasKey(c => c.Dia);

            });
        });

        modelBuilder.Entity<Ponto>()
            .ToContainer("Pontos")
            .HasPartitionKey(x => x.PartitionKey);

        modelBuilder.Entity<Ponto>().Property(x => x.Pausa).HasConversion(new EnumToStringConverter<PausaEnum>());

        modelBuilder.Entity<Ponto>().Property(x => x.Version).IsETagConcurrency();

        modelBuilder.Entity<Comprovante>()
            .ToContainer("Pontos")
            .HasPartitionKey(x => x.PartitionKey);

        modelBuilder.Entity<Comprovante>().Property(x => x.Version).IsETagConcurrency();

        modelBuilder.Entity<Trabalhador>()
            .ToContainer("Trabalhadores")
            .HasNoDiscriminator()
            .HasKey(x => x.UserName);

        modelBuilder.Entity<ConfiguracaoPorUsuario>()
            .ToContainer("Configuracoes")
            .HasNoDiscriminator()
            .HasKey(x => x.UserName);
    }

    public DbSet<Perfil> Perfis { get; set; }
    public DbSet<Empresa> Empresas { get; set; }
    public DbSet<Folha> Folhas { get; set; }
    public DbSet<Ponto> Pontos { get; set; }
    public DbSet<Comprovante> Comprovantes { get; set; }
    public DbSet<Trabalhador> Trabalhadores { get; set; }
    public DbSet<ConfiguracaoPorUsuario> Configuracoes { get; set; }
}
