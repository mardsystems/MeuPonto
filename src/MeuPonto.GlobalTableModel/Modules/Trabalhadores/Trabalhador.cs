using MeuPonto.Billing;
using System.ComponentModel;

namespace MeuPonto.Modules.Trabalhadores;

public class Trabalhador : GlobalTableEntity
{
    [DisplayName("Assinatura do Cliente")]
    public CustomerSubscription? CustomerSubscription { get; set; }

    public string? UserId { get; set; }
}
