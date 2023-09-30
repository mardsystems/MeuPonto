using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MeuPonto.Models;

public class Perfil : LocalTableEntity
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

    [DisplayName("Jornada Trabalho Semanal Prevista")]
    public virtual JornadaTrabalhoSemanal JornadaTrabalhoSemanalPrevista { get; set; } = default!;

    public string? UserId { get; set; }

    public Perfil()
    {
        JornadaTrabalhoSemanalPrevista = new JornadaTrabalhoSemanal();
    }
}

[Owned]
public class JornadaTrabalhoSemanal
{
    [DisplayName("Semana")]
    public virtual IList<JornadaTrabalhoDiaria> Semana { get; set; } = default!;

    [DisplayName("Tempo Total")]
    [DisplayFormat(DataFormatString = "{0:d\\d\\ hh\\:mm}")]
    public TimeSpan TempoTotal
    {
        get
        {
            TimeSpan tempoTotal = TimeSpan.Zero;

            foreach (var jornadaTrabalhoDiaria in Semana)
            {
                tempoTotal += jornadaTrabalhoDiaria.Tempo ?? TimeSpan.Zero;
            }

            return tempoTotal;
        }
    }

    public JornadaTrabalhoSemanal()
    {
        Semana = new List<JornadaTrabalhoDiaria>();
    }
}

[Owned]
public class JornadaTrabalhoDiaria
{
    [Required]
    [DisplayName("Dia Semana")]
    public DayOfWeek? DiaSemana { get; set; }

    [Required]
    [DisplayName("Tempo")]
    [DisplayFormat(DataFormatString = "{0:hh\\:mm}", ApplyFormatInEditMode = true)]
    public TimeSpan? Tempo { get; set; }
}
