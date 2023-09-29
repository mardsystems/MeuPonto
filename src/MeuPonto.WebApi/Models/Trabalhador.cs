using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MeuPonto.Models;

public class Trabalhador : LocalTableEntity, Concepts.Trabalhador
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

    Concepts.Perfil[] Concepts.Trabalhador.Cadastra()
    {
        throw new NotImplementedException();
    }

    Concepts.Folha[] Concepts.Trabalhador.Gerencia()
    {
        throw new NotImplementedException();
    }

    Concepts.Ponto[] Concepts.Trabalhador.Registra()
    {
        throw new NotImplementedException();
    }
}
