using System.ComponentModel.DataAnnotations;

namespace MeuPonto.Models;

public class PontoComprovanteImagemTipo : Concepts.TipoImagem
{
    public int Id { get; set; }

    [MaxLength(255)]
    public string? Nome { get; set; }
}
