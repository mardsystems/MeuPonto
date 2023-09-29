using MeuPonto.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MeuPonto.Models;

public class Comprovante : LocalTableEntity
{
    [Required]
    [DisplayName("Ponto")]
    public int? PontoId { get; set; }

    [DisplayName("Ponto")]
    public Ponto? Ponto { get; set; }

    [MaxLength(16)]
    [DisplayName("Número")]
    public string? Numero { get; set; }

    [Required]
    [DisplayName("Imagem")]
    public byte[]? Imagem { get; set; }

    [Required]
    [DisplayName("Tipo Imagem")]
    public TipoImagemEnum? TipoImagemId { get; set; }

    [DisplayName("Tipo Imagem")]
    public TipoImagem? TipoImagem { get; set; }

    public string? UserId { get; set; }

    public Comprovante()
    {
        Imagem = new byte[0];
    }
}
