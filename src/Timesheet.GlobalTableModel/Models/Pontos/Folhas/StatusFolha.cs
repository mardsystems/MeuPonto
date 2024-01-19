using System.ComponentModel.DataAnnotations;

namespace Timesheet.Models.Pontos.Folhas;

public class StatusFolha
{
    public StatusFolhaEnum Id { get; set; }

    [MaxLength(255)]
    public string Nome { get; set; } = default!;
}
