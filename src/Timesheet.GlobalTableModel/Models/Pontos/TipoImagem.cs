using System.ComponentModel.DataAnnotations;

namespace Timesheet.Models.Pontos;

public class TipoImagem
{
    public TipoImagemEnum Id { get; set; }

    [MaxLength(255)]
    public string Nome { get; set; } = default!;
}
