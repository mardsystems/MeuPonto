using System.ComponentModel.DataAnnotations;

namespace MeuPonto.Modules.Pontos.Folhas;

public class Status
{
    public StatusEnum Id { get; set; }

    [MaxLength(255)]
    public string? Nome { get; set; }
}
