using System.ComponentModel;

namespace MeuPonto.Models;

public class Trabalhador : LocalTableEntity, Concepts.Trabalhador
{
    [DisplayName("Assinatura do Cliente")]
    public CustomerSubscription? CustomerSubscription { get; set; }

    public string? UserId { get; set; }

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
