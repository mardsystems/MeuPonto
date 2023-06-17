using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MeuPonto.Modules.Pontos.Comprovantes;

[Owned]
public class Ponto : Concepts.Ponto
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

    bool Concepts.Ponto.Estimado => throw new NotImplementedException();

    string? Concepts.Ponto.Observacao => throw new NotImplementedException();
}
