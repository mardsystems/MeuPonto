using MeuPonto.Modules.Perfis;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MeuPonto.Models;

public class Perfil : IdentityTableEntity, Perfil_, MeuPonto.Modules.Pontos.Perfil_
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
    Empresa_? Perfil_.Empresa => (Empresa_)Empresa;

    //[DisplayName("Jornada Trabalho Semanal Prevista")]
    //public virtual JornadaTrabalhoSemanal JornadaTrabalhoSemanalPrevista { get; set; } = default!;
    //JornadaTrabalhoSemanal_ Perfil_.JornadaTrabalhoSemanalPrevista => JornadaTrabalhoSemanalPrevista;
    public JornadaTrabalhoSemanal_ JornadaTrabalhoSemanalPrevista => throw new NotImplementedException();

    public Perfil()
    {
        //JornadaTrabalhoSemanalPrevista = new JornadaTrabalhoSemanal();
    }
}
