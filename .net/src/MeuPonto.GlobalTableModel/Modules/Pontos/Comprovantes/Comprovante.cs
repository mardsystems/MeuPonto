using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MeuPonto.Modules.Pontos.Comprovantes;

public class Comprovante : GlobalTableEntity, Concepts.Comprovante
{
    [Required]
    [DisplayName("Ponto")]
    public Guid? PontoId { get; set; }

    [DisplayName("Ponto")]
    public Ponto? Ponto { get; set; }
    Concepts.Ponto? Concepts.Comprovante.Ponto => Ponto;

    [MaxLength(16)]
    [DisplayName("Número")]
    public string? Numero { get; set; }

    [Required]
    [DisplayName("Imagem")]
    public byte[]? Imagem { get; set; }

    [Required]
    [DisplayName("Tipo Imagem")]
    public TipoImagemEnum? TipoImagemId { get; set; }
    Concepts.TipoImagem? Concepts.Comprovante.TipoImagem => new TipoImagem(TipoImagemId);
}
