using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MeuPonto.Models;

public class Empregador : IdentityTableEntity, Concepts.Empregador
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

    //[Required]
    [StringLength(11)]
    [DisplayName("CPF")]
    public string? Cpf { get; set; }

    [StringLength(12)]
    [DisplayName("Inscrição Estadual")]
    public string? InscricaoEstadual { get; set; }

    [MaxLength(36)]
    [DisplayName("Endereço")]
    public string? Endereco { get; set; }
}
