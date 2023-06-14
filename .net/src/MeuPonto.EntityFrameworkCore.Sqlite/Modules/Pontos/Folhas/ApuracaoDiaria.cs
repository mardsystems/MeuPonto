﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MeuPonto.Modules.Pontos.Folhas;

[Owned]
public class ApuracaoDiaria : ApuracaoDiaria_
{
    [Required]
    [DisplayName("Dia")]
    public int? Dia { get; set; }

    [Required]
    [DisplayName("Tempo Previsto")]
    [DisplayFormat(DataFormatString = "{0:hh\\:mm}", ApplyFormatInEditMode = true)]
    public TimeSpan? TempoPrevisto { get; set; }

    [DisplayName("Tempo Apurado")]
    [DisplayFormat(DataFormatString = "{0:hh\\:mm}", ApplyFormatInEditMode = true)]
    public TimeSpan? TempoApurado { get; set; }

    [DisplayName("Diferença Tempo")]
    [DisplayFormat(DataFormatString = "{0:hh\\:mm}", ApplyFormatInEditMode = true)]
    public TimeSpan? DiferencaTempo { get; set; }

    [DisplayName("Tempo Abonado")]
    [DisplayFormat(DataFormatString = "{0:hh\\:mm}", ApplyFormatInEditMode = true)]
    public TimeSpan? TempoAbonado { get; set; }

    [Required]
    [DisplayName("Feriado?")]
    public bool Feriado { get; set; }

    [Required]
    [DisplayName("Falta?")]
    public bool Falta { get; set; }
}
