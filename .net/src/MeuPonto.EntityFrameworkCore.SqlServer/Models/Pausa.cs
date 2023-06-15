using MeuPonto.Modules.Pontos;
using System.ComponentModel.DataAnnotations;

namespace MeuPonto.Models;

public class Pausa : Concepts.Pausa
{
    public PausaEnum Id { get; set; }

    [MaxLength(255)]
    public string? Nome { get; set; }
}
