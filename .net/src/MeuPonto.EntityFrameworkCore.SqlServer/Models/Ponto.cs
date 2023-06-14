using MeuPonto.Modules.Pontos;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MeuPonto.Models;

public class Ponto : IdentityTableEntity, Ponto_, Modules.Pontos.Comprovantes.Ponto_
{
    [Required]
    [DisplayName("Perfil")]
    public int? PerfilId { get; set; }

    [DisplayName("Perfil")]
    public Perfil? Perfil { get; set; }
    Perfil_? Ponto_.Perfil => Perfil;
    Perfil_? Modules.Pontos.Comprovantes.Ponto_.Perfil => Perfil;

    [Required]
    [DisplayName("Data/Hora")]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
    public DateTime? DataHora { get; set; }

    [Required]
    [DisplayName("Momento")]
    public MomentoEnum? Momento { get; set; }
    Momento_? Ponto_.Momento => throw new NotImplementedException();
    Momento_? Modules.Pontos.Comprovantes.Ponto_.Momento => throw new NotImplementedException();

    [DisplayName("Pausa")]
    public PausaEnum? Pausa { get; set; }
    Pausa_? Ponto_.Pausa => throw new NotImplementedException();
    Pausa_? Modules.Pontos.Comprovantes.Ponto_.Pausa => throw new NotImplementedException();

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
