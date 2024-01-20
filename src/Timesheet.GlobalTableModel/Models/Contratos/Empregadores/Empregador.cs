using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Timesheet.Models.Contratos.Empregadores;

public class Empregador : GlobalTableEntity
{
    [Required]
    [MinLength(3)]
    [MaxLength(35)]
    [DisplayName("Nome")]
    public string? Nome { get; set; }

    public string? UserId { get; set; }
}
