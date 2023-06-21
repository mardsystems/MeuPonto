using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MeuPonto.Modules.Pontos.Comprovantes;

[Owned]
public class PontoRef : Concepts.Ponto
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

    bool Concepts.Ponto.Estimado => throw new NotImplementedException();

    string? Concepts.Ponto.Observacao => throw new NotImplementedException();
}
