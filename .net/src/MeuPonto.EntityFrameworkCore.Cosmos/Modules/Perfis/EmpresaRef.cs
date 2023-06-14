using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MeuPonto.Modules.Perfis;

[Owned]
public class EmpresaRef : Empresa_
{
    [Required]
    [MinLength(3)]
    [MaxLength(36)]
    [DisplayName("Nome")]
    public string? Nome { get; set; }

    //[Required]
    [StringLength(14)]
    [DisplayName("CNPJ")]
    public string? Cnpj { get; set; }
}
