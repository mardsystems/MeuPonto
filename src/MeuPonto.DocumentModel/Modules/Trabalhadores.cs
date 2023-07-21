using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using MeuPonto.Concepts;

namespace MeuPonto.Modules.Trabalhadores;

public class Trabalhador : Concepts.Trabalhador
{
    public Guid UserId { get; set; }

    [Required]
    [MinLength(3)]
    [MaxLength(36)]
    [DisplayName("Nome")]
    public string? Nome { get; set; }

    [StringLength(12)]
    [DisplayName("PIS")]
    public string? Pis { get; set; }

    public DateTime? CreationDate { get; set; }

    public string? Version { get; set; }

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

public static class TrabalhadorFactory
{
    public static Trabalhador CriaTrabalhador(TransactionContext transaction)
    {
        var trabalhador = new Trabalhador
        {
            UserId = transaction.UserId,
            Nome = transaction.UserName,
            CreationDate = transaction.DateTime
        };

        return trabalhador;
    }

    public static void RecontextualizaTrabalhador(this Trabalhador trabalhador, TransactionContext transaction)
    {
        trabalhador.UserId = trabalhador.UserId;
        trabalhador.CreationDate = transaction.DateTime;
    }
}
