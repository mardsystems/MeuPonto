using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MeuPonto.Modules.Pontos;

public class Ponto : DocumentEntity, Concepts.Ponto
{
    [Required]
    [DisplayName("Perfil")]
    public Guid? PerfilId { get; set; }

    [DisplayName("Perfil")]
    public PerfilRef? Perfil { get; set; }
    Concepts.Perfil? Concepts.Ponto.Perfil => Perfil;

    [Required]
    [DisplayName("Data/Hora")]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
    public DateTime? DataHora { get; set; }

    [Required]
    [DisplayName("Momento")]
    public MomentoEnum? MomentoId { get; set; }
    string? Concepts.Ponto.Momento => MomentoId?.GetDisplayName();

    [DisplayName("Pausa")]
    public PausaEnum? PausaId { get; set; }
    string? Concepts.Ponto.Pausa => PausaId?.GetDisplayName();

    [Required]
    [DisplayName("Estimado?")]
    public bool Estimado { get; set; }

    [MinLength(3)]
    [MaxLength(255)]
    [DisplayName("Observação")]
    public string? Observacao { get; set; }
}
