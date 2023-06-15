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
    public MomentoEnum? Momento { get; set; }
    Concepts.Momento? Concepts.Ponto.Momento => Momento == null ? null : new Momento(Momento.Value);

    [DisplayName("Pausa")]
    public PausaEnum? Pausa { get; set; }
    Concepts.Pausa? Concepts.Ponto.Pausa => Pausa == null ? null : new Pausa(Pausa.Value);

    [Required]
    [DisplayName("Estimado?")]
    public bool Estimado { get; set; }

    [MinLength(3)]
    [MaxLength(255)]
    [DisplayName("Observação")]
    public string? Observacao { get; set; }
}
