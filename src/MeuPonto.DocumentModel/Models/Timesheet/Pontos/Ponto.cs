using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MeuPonto.Models.Timesheet.Pontos;

public class Ponto : DocumentEntity
{
    [Required]
    [DisplayName("Contrato")]
    public Guid? ContratoId { get; set; }

    [DisplayName("Contrato")]
    public ContratoRef? Contrato { get; set; }

    [Required]
    [DisplayName("Data/Hora")]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
    public DateTime? DataHora { get; set; }

    [Required]
    [DisplayName("Momento")]
    public MomentoEnum? MomentoId { get; set; }

    [DisplayName("Pausa")]
    public PausaEnum? PausaId { get; set; }

    [Required]
    [DisplayName("Estimado?")]
    public bool Estimado { get; set; }

    [MinLength(3)]
    [MaxLength(255)]
    [DisplayName("Observação")]
    public string? Observacao { get; set; }

    public string? UserId { get; set; }
}
