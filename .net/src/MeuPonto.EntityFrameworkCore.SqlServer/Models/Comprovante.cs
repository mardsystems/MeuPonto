using MeuPonto.Modules.Pontos.Comprovantes;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MeuPonto.Models;

public class Comprovante : IdentityTableEntity, Concepts.Comprovante
{
    [Required]
    [DisplayName("Ponto")]
    public int? PontoId { get; set; }

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
    public TipoImagemEnum? TipoImagem { get; set; }
    public int? ImagemTipoId { get; set; }
    public TipoImagem? ImagemTipo { get; set; }
    Concepts.TipoImagem? Concepts.Comprovante.TipoImagem => ImagemTipo;

    public Comprovante()
    {
        Imagem = new byte[0];
    }
}
