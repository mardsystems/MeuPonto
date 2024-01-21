using MeuPonto.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Timesheet.Models.Pontos;
using Timesheet.Models.Contratos;
using Timesheet.Models.Folhas;
using MeuPonto.Models.Trabalhadores;

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
        var empregadoresAlterados = ChangeTracker
            .Entries<Empregador>()
            .Where(x => x.State == EntityState.Modified)
            .Select(x => x.Entity);

        foreach (var empregadorAlterado in empregadoresAlterados)
        {
            var contratos = Contratos.Where(x => x.EmpregadorId == empregadorAlterado.Id);

            foreach (var contrato in contratos)
            {
                contrato.Empregador.Nome = empregadorAlterado.Nome;
            }
        }

        var contratosAlterados = ChangeTracker
        .Entries<Contrato>()
        .Where(x => x.State == EntityState.Modified)
        .Select(x => x.Entity);

        foreach (var contratoAlterado in contratosAlterados)
        {
            var pontos = Pontos.Where(x => x.ContratoId == contratoAlterado.Id);

            foreach (var ponto in pontos)
            {
                ponto.Contrato.Nome = contratoAlterado.Nome;
            }

            var folhas = Folhas.Where(x => x.ContratoId == contratoAlterado.Id);

            foreach (var folha in folhas)
            {
                folha.Contrato.Nome = contratoAlterado.Nome;
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
                comprovante.Ponto.ContratoId = pontoAlterado.ContratoId;
                comprovante.Ponto.Contrato = pontoAlterado.Contrato;
                comprovante.Ponto.DataHora = pontoAlterado.DataHora;
                comprovante.Ponto.MomentoId = pontoAlterado.MomentoId;
                comprovante.Ponto.PausaId = pontoAlterado.PausaId;
            }
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Configuracoes>()
            .ToContainer("Configuracoes")
            .HasNoDiscriminator()
            .HasPartitionKey(x => x.UserId)
            .HasKey(x => x.UserId);

        modelBuilder.Entity<Trabalhador>()
            .ToContainer("Trabalhadores")
            .HasPartitionKey(x => x.PartitionKey);

        modelBuilder.Entity<Trabalhador>().Property(x => x.Version).IsETagConcurrency();

        modelBuilder.Entity<Contrato>()
            .ToContainer("Contratos")
            .HasPartitionKey(x => x.PartitionKey);
        //.HasKey(x => new { x.Id, x.PartitionKey });

        modelBuilder.Entity<Contrato>().Property(x => x.Version).IsETagConcurrency();

        modelBuilder.Entity<Contrato>().OwnsOne(a => a.JornadaTrabalhoSemanalPrevista, x =>
        {
            x.OwnsMany(b => b.Semana, y =>
            {
                //y.HasKey(c => c.DiaSemana);
            });
        });

        modelBuilder.Entity<Empregador>()
            .ToContainer("Contratos")
            .HasPartitionKey(x => x.PartitionKey);

        modelBuilder.Entity<Empregador>().Property(x => x.Version).IsETagConcurrency();

        modelBuilder.Entity<Ponto>()
            .ToContainer("Pontos")
            .HasPartitionKey(x => x.PartitionKey);

        modelBuilder.Entity<Ponto>().Property(x => x.PausaId).HasConversion(new EnumToStringConverter<PausaEnum>());

        modelBuilder.Entity<Ponto>().Property(x => x.Version).IsETagConcurrency();

        modelBuilder.Entity<Comprovante>()
            .ToContainer("Pontos")
            .HasPartitionKey(x => x.PartitionKey);

        modelBuilder.Entity<Comprovante>().Property(x => x.Version).IsETagConcurrency();

        modelBuilder.Entity<Folha>()
            .ToContainer("Folhas")
            .HasPartitionKey(x => x.PartitionKey);

        modelBuilder.Entity<Folha>().Property(x => x.Version).IsETagConcurrency();

        modelBuilder.Entity<Folha>().OwnsOne(a => a.ApuracaoMensal, x =>
        {
            x.OwnsMany(b => b.Dias, y =>
            {
                //y.HasKey(c => c.Dia);

            });
        });
    }

    public DbSet<Configuracoes> Configuracoes { get; set; }
    public DbSet<Trabalhador> Trabalhadores { get; set; }
    public DbSet<Empregador> Empregadores { get; set; }
    public DbSet<Contrato> Contratos { get; set; }
    public DbSet<Ponto> Pontos { get; set; }
    public DbSet<Comprovante> Comprovantes { get; set; }
    public DbSet<Folha> Folhas { get; set; }
}
