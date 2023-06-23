using MeuPonto.Enums;
using System.ComponentModel.DataAnnotations;

namespace MeuPonto.Models;

public class Pausa
{
    public PausaEnum Id { get; set; }

    [MaxLength(255)]
    public string? Nome { get; set; }
}
