using System.ComponentModel.DataAnnotations;

namespace MeuPonto.Models.Pontos;

public class Pausa
{
    public PausaEnum Id { get; set; }

    [MaxLength(255)]
    public string Nome { get; set; } = default!;
}
