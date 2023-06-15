using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MeuPonto.Models;

[Owned]
public class PerfilJornadaTrabalhoDiaria : Concepts.JornadaTrabalhoDiaria
{
    [Required]
    [DisplayName("Dia Semana")]
    public DayOfWeek? DiaSemana { get; set; }

    [Required]
    [DisplayName("Tempo")]
    [DisplayFormat(DataFormatString = "{0:hh\\:mm}", ApplyFormatInEditMode = true)]
    public TimeSpan? Tempo { get; set; }
}
