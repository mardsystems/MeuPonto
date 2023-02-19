using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MeuPonto.Modules.Pontos.Comprovantes;

public class Comprovante : DocumentEntity, Comprovante_
{
    [Required]
    [DisplayName("Ponto")]
    public Guid? PontoId { get; set; }

    [DisplayName("Ponto")]
    public PontoRef? Ponto { get; set; }
    Ponto_? Comprovante_.Ponto => Ponto;

    [MaxLength(16)]
    [DisplayName("Número")]
    public string? Numero { get; set; }

    [Required]
    [DisplayName("Imagem")]
    public byte[]? Imagem { get; set; }

    [Required]
    [DisplayName("Tipo Imagem")]
    public TipoImagemEnum? TipoImagem { get; set; }
    TipoImagem_? Comprovante_.TipoImagem => TipoImagem == null ? null : new TipoImagem(TipoImagem.Value);
}
