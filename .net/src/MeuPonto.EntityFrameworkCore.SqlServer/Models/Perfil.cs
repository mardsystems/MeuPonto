using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MeuPonto.Models;

public class Perfil : IdentityTableEntity, Concepts.Perfil
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
    public PerfilEmpresa? Empresa { get; set; }
    Concepts.Empresa? Concepts.Perfil.Empresa => (Concepts.Empresa)Empresa;

    [DisplayName("Jornada Trabalho Semanal Prevista")]
    public virtual PerfilJornadaTrabalhoSemanal JornadaTrabalhoSemanalPrevista { get; set; } = default!;
    Concepts.JornadaTrabalhoSemanal Concepts.Perfil.JornadaTrabalhoSemanalPrevista => JornadaTrabalhoSemanalPrevista;

    public Perfil()
    {
        JornadaTrabalhoSemanalPrevista = new PerfilJornadaTrabalhoSemanal();
    }
}
