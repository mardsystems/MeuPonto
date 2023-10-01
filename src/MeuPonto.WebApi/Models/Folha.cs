using MeuPonto.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeuPonto.Models;

public class Folha : GlobalTableEntity
{
    [Required]
    [DisplayName("Perfil")]
    public int? PerfilId { get; set; }

    [DisplayName("Perfil")]
    public Perfil? Perfil { get; set; }

    [Required]
    [DisplayName("Competência")]
    [DisplayFormat(DataFormatString = "{0:y}")]
    public DateTime? Competencia { get; set; }

    [Required]
    [DisplayName("Status")]
    public StatusFolhaEnum? StatusId { get; set; }

    [DisplayName("Status")]
    public StatusFolha? Status { get; set; }

    [MinLength(3)]
    [MaxLength(255)]
    [DisplayName("Observação")]
    public string? Observacao { get; set; }

    [DisplayName("Apuração Mensal")]
    public ApuracaoMensal ApuracaoMensal { get; set; }

    public string? UserId { get; set; }

    public Folha()
    {
        ApuracaoMensal = new ApuracaoMensal();
    }
}

[Owned]
public class ApuracaoMensal
{
    [DisplayName("Dias")]
    public IList<ApuracaoDiaria> Dias { get; set; }

    [DisplayName("Total Dias")]
    public int TotalDias { get => Dias.Count; }

    [DisplayName("Tempo Total Previsto")]
    //[DisplayFormat(DataFormatString = "{0:d\\d\\ hh\\:mm}")]
    [NotMapped]
    public TimeSpan? TempoTotalPrevisto { get; set; }

    [DisplayName("Tempo Total Apurado")]
    //[DisplayFormat(DataFormatString = "{0:d\\d\\ hh\\:mm}")]
    [NotMapped]
    public TimeSpan? TempoTotalApurado { get; set; }

    [DisplayName("Diferença Tempo Total")]
    //[DisplayFormat(DataFormatString = "{0:d\\d\\ hh\\:mm}")]
    [NotMapped]
    public TimeSpan? DiferencaTempoTotal { get; set; }

    [DisplayName("Tempo Total Período Anterior")]
    //[DisplayFormat(DataFormatString = "{0:d\\d\\ hh\\:mm}")]
    public TimeSpan? TempoTotalPeriodoAnterior { get; set; }

    public ApuracaoMensal()
    {
        Dias = new List<ApuracaoDiaria>();
    }
}

[Owned]
public class ApuracaoDiaria
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

    [MinLength(3)]
    [MaxLength(255)]
    [DisplayName("Observação")]
    public string? Observacao { get; set; }
}
