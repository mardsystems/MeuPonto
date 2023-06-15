using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MeuPonto.Modules;

public class Trabalhador : Concepts.Trabalhador
{
    public string UserName { get; set; }

    [Required]
    [MinLength(3)]
    [MaxLength(36)]
    [DisplayName("Nome")]
    public string? Nome { get; set; }

    [StringLength(12)]
    [DisplayName("PIS")]
    public string? Pis { get; set; }
}
