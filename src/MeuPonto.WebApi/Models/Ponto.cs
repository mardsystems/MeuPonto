using MeuPonto.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MeuPonto.Models;

public class Ponto : LocalTableEntity, Concepts.Ponto
{
    [Required]
    [DisplayName("Perfil")]
    public int? PerfilId { get; set; }

    [DisplayName("Perfil")]
    public Perfil? Perfil { get; set; }
    Concepts.Perfil? Concepts.Ponto.Perfil => Perfil;

    [Required]
    [DisplayName("Data/Hora")]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
    public DateTime? DataHora { get; set; }

    [Required]
    [DisplayName("Momento")]
    public MomentoEnum? MomentoId { get; set; }

    [DisplayName("Momento")]
    public Momento? Momento { get; set; }
    string? Concepts.Ponto.Momento => Momento?.Nome;

    [DisplayName("Pausa")]
    public PausaEnum? PausaId { get; set; }
    
    [DisplayName("Pausa")]
    public Pausa? Pausa { get; set; }
    string? Concepts.Ponto.Pausa => Pausa?.Nome;
    
    [Required]
    [DisplayName("Estimado?")]
    public bool Estimado { get; set; }

    [MinLength(3)]
    [MaxLength(255)]
    [DisplayName("Observação")]
    public string? Observacao { get; set; }

    [DisplayName("Comprovantes")]
    public virtual IList<Comprovante> Comprovantes { get; set; } = default!;

    public string? UserId { get; set; }

    public Ponto()
    {
        Comprovantes = new List<Comprovante>();
    }
}
