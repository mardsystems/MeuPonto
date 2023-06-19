using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using MeuPonto.Concepts;

namespace MeuPonto.Modules.Perfis;

[Owned]
public class Empregador : Concepts.Empregador
{
    [Required]
    [MinLength(3)]
    [MaxLength(36)]
    [DisplayName("Nome")]
    public string? Nome { get; set; }

    string? Concepts.Empregador.Cnpj => throw new NotImplementedException();

    string? Concepts.Empregador.Cpf => throw new NotImplementedException();

    string? Concepts.Empregador.Endereco => throw new NotImplementedException();

    string? Concepts.Empregador.InscricaoEstadual => throw new NotImplementedException();
}
