using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using MeuPonto.Concepts;

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

    Perfil[] Concepts.Trabalhador.Cadastra()
    {
        throw new NotImplementedException();
    }

    Folha[] Concepts.Trabalhador.Gerencia()
    {
        throw new NotImplementedException();
    }

    Ponto[] Concepts.Trabalhador.Registra()
    {
        throw new NotImplementedException();
    }
}
