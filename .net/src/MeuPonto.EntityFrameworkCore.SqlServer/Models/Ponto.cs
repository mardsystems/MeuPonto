using MeuPonto.Modules.Pontos;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MeuPonto.Models;

public class Ponto : IdentityTableEntity, Concepts.Ponto
{
    [Required]
    [DisplayName("Perfil")]
    public int? PerfilId { get; set; }

    [DisplayName("Perfil")]
    public Perfil? Perfil { get; set; }
    Concepts.Perfil? Concepts.Ponto.EQualificadoPelo() => Perfil;
    
    [Required]
    [DisplayName("Data/Hora")]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
    public DateTime? DataHora { get; set; }

    [Required]
    [DisplayName("Momento")]
    public MomentoEnum? MomentoId { get; set; }

    [DisplayName("Momento")]
    public Momento? Momento { get; set; }
    Concepts.Momento? Concepts.Ponto.Momento => Momento;

    [DisplayName("Pausa")]
    public PausaEnum? PausaId { get; set; }
    
    [DisplayName("Pausa")]
    public Pausa? Pausa { get; set; }
    Concepts.Pausa? Concepts.Ponto.Pausa => Pausa;
    
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

    Concepts.Comprovante[] Concepts.Ponto.Guarda()
    {
        throw new NotImplementedException();
    }
}
