using System.ComponentModel.DataAnnotations;

namespace MeuPonto.Models;

public class PontoComprovanteViewModel
{
    public int? Id { get; set; }

    public DateTime? CreationDate { get; set; }

    [Timestamp]
    public byte[]? Version { get; set; }

    [Required]
    public int? PontoId { get; set; }

    public virtual Ponto? Ponto { get; set; }

    [MaxLength(16)]
    public string? Numero { get; set; }

    [Required]
    public IFormFile? Imagem { get; set; }

    [Required]
    public int? ImagemTipoId { get; set; }

    public virtual PontoComprovanteImagemTipo? ImagemTipo { get; set; }
}
