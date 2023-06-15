using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using MeuPonto.Modules.Perfis.Empresas;

namespace MeuPonto.Modules.Perfis;

public class Perfil : GlobalTableEntity, Concepts.Perfil
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

    [DisplayName("Empresa")]
    public Guid? EmpresaId { get; set; }

    [DisplayName("Empresa")]
    public Empresa? Empresa { get; set; }
    Concepts.Empresa? Concepts.Perfil.Empresa => Empresa;

    [DisplayName("Jornada Trabalho Semanal Prevista")]
    public virtual JornadaTrabalhoSemanal JornadaTrabalhoSemanalPrevista { get; set; } = default!;
    Concepts.JornadaTrabalhoSemanal Concepts.Perfil.JornadaTrabalhoSemanalPrevista => JornadaTrabalhoSemanalPrevista;

    public Perfil()
    {
        JornadaTrabalhoSemanalPrevista = new JornadaTrabalhoSemanal();
    }
}
