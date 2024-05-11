using System.ComponentModel.DataAnnotations;

namespace MeuPonto.Models.Pontos;

public class Momento
{
    public MomentoEnum Id { get; set; }

    [MaxLength(255)]
    public string Nome { get; set; } = default!;
}
