using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MeuPonto.Models.Timesheet.Pontos.Comprovantes;

[Owned]
public class PontoRef
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
}
