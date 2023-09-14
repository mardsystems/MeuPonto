using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using MeuPonto.Modules.Empregadores;

namespace MeuPonto.Modules.Perfis;

public class Perfil : GlobalTableEntity, Concepts.Perfil, Concepts.Contrato
{
    [Required]
    [MinLength(3)]
    [MaxLength(36)]
    [DisplayName("Nome")]
    public string? Nome { get; set; }

    [Required]
    [DisplayName("Ativo?")]
    public bool Ativo { get; set; }

    [DisplayName("Empregador")]
    public Guid? EmpregadorId { get; set; }

    [DisplayName("Empregador")]
    public Empregador? Empregador { get; set; }
    Concepts.Empregador? Concepts.Contrato.Vincula() => Empregador;

    [DisplayName("Jornada Trabalho Semanal Prevista")]
    public virtual JornadaTrabalhoSemanal JornadaTrabalhoSemanalPrevista { get; set; } = default!;
    Concepts.JornadaTrabalhoSemanal Concepts.Contrato.Preve() => JornadaTrabalhoSemanalPrevista;

    public Guid? TrabalhadorId { get; set; }

    Concepts.Contrato Concepts.Perfil.IdentificaVinculo() => this;

    public Perfil()
    {
        JornadaTrabalhoSemanalPrevista = new JornadaTrabalhoSemanal();
    }
}
