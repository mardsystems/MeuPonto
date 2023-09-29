using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MeuPonto.Modules.Empregadores;

public class Empregador : GlobalTableEntity, Concepts.Empregador
{
    [Required]
    [MinLength(3)]
    [MaxLength(36)]
    [DisplayName("Nome")]
    public string? Nome { get; set; }

    public string? UserId { get; set; }
}
