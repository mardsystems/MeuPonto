using System.ComponentModel.DataAnnotations;

namespace MeuPonto.Modules.Pontos;

public class Pausa
{
    public PausaEnum Id { get; set; }

    [MaxLength(255)]
    public string? Nome { get; set; }
}
