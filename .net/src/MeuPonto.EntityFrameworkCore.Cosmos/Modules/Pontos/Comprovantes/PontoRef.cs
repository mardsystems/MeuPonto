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
    public MomentoEnum? Momento { get; set; }
    Concepts.Momento? Concepts.Ponto.Momento => Momento == null ? null : new Momento(Momento.Value);

    [DisplayName("Pausa")]
    public PausaEnum? Pausa { get; set; }
    Concepts.Pausa? Concepts.Ponto.Pausa => Pausa == null ? null : new Pausa(Pausa.Value);

    bool Concepts.Ponto.Estimado => throw new NotImplementedException();

    string? Concepts.Ponto.Observacao => throw new NotImplementedException();
}
