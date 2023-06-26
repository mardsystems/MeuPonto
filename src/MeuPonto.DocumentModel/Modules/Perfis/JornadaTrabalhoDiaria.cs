using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MeuPonto.Modules.Perfis;

[Owned]
public class JornadaTrabalhoDiaria : Concepts.JornadaTrabalhoDiaria
{
    [Required]
    [DisplayName("Dia Semana")]
    public DayOfWeek? DiaSemana { get; set; }

    [Required]
    [DisplayName("Tempo")]
    [DisplayFormat(DataFormatString = "{0:hh\\:mm}", ApplyFormatInEditMode = true)]
    public TimeSpan? Tempo { get; set; }
}
