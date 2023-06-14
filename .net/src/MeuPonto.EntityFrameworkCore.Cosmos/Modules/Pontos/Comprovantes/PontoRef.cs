using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MeuPonto.Modules.Pontos.Comprovantes;

[Owned]
public class PontoRef : Ponto_
{
    [Required]
    [DisplayName("Perfil")]
    public Guid? PerfilId { get; set; }

    [DisplayName("Perfil")]
    public PerfilRef? Perfil { get; set; }
    Perfil_? Ponto_.Perfil => Perfil;

    [Required]
    [DisplayName("Data/Hora")]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
    public DateTime? DataHora { get; set; }

    [Required]
    [DisplayName("Momento")]
    public MomentoEnum? Momento { get; set; }
    Momento_? Ponto_.Momento => Momento == null ? null : new Momento(Momento.Value);

    [DisplayName("Pausa")]
    public PausaEnum? Pausa { get; set; }
    Pausa_? Ponto_.Pausa => Pausa == null ? null : new Pausa(Pausa.Value);
}
