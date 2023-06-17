using MeuPonto.Modules.Perfis;
using MeuPonto.Modules.Pontos.Comprovantes;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MeuPonto.Modules.Pontos;

public class Ponto : GlobalTableEntity, Concepts.Ponto
{
    [Required]
    [DisplayName("Perfil")]
    public Guid? PerfilId { get; set; }

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
    Concepts.Momento? Concepts.Ponto.Momento => MomentoId == null ? null : new Momento(MomentoId.Value);

    [DisplayName("Pausa")]
    public PausaEnum? PausaId { get; set; }
    Concepts.Pausa? Concepts.Ponto.Pausa => PausaId == null ? null : new Pausa(PausaId.Value);

    [Required]
    [DisplayName("Estimado?")]
    public bool Estimado { get; set; }

    [MinLength(3)]
    [MaxLength(255)]
    [DisplayName("Observação")]
    public string? Observacao { get; set; }

    [DisplayName("Comprovantes")]
    public virtual IList<Comprovante> Comprovantes { get; set; } = default!;

    public Ponto()
    {
        Comprovantes = new List<Comprovante>();
    }
}
