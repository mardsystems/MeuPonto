using MeuPonto.Enums;
using System.ComponentModel.DataAnnotations;

namespace MeuPonto.Models;

public class TipoImagem : Concepts.TipoImagem
{
    public TipoImagemEnum Id { get; set; }

    [MaxLength(255)]
    public string? Nome { get; set; }
}
