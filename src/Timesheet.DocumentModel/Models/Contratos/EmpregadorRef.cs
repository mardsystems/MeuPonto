using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Timesheet.Models.Contratos;

[Owned]
public class EmpregadorRef
{
    [Required]
    [MinLength(3)]
    [MaxLength(35)]
    [DisplayName("Nome")]
    public string? Nome { get; set; }

    public override string ToString()
    {
        return Nome;
    }
}
