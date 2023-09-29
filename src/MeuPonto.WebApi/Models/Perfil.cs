using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MeuPonto.Models;

public class Perfil : LocalTableEntity, Concepts.Perfil, Concepts.Contrato
{
    [Required]
    [MinLength(3)]
    [MaxLength(36)]
    [DisplayName("Nome")]
    public string? Nome { get; set; }

    [Required]
    [DisplayName("Ativo?")]
    public bool Ativo { get; set; }

    [MaxLength(30)]
    [DisplayName("Matrícula")]
    public string? Matricula { get; set; }

    [DisplayName("Empregador")]
    public int? EmpregadorId { get; set; }

    [DisplayName("Empregador")]
    public Empregador? Empregador { get; set; }
    Concepts.Empregador? Concepts.Contrato.Vincula() => Empregador;

    [DisplayName("Jornada Trabalho Semanal Prevista")]
    public virtual JornadaTrabalhoSemanal JornadaTrabalhoSemanalPrevista { get; set; } = default!;
    Concepts.JornadaTrabalhoSemanal Concepts.Contrato.Preve()=> JornadaTrabalhoSemanalPrevista;

    public int? TrabalhadorId { get; set; }

    Concepts.Contrato Concepts.Perfil.IdentificaVinculo() => this;

    public Perfil()
    {
        JornadaTrabalhoSemanalPrevista = new JornadaTrabalhoSemanal();
    }
}
