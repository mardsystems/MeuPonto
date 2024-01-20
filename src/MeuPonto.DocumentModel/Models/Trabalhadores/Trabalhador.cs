using Billing.Models;
using System.ComponentModel;

namespace MeuPonto.Models.Trabalhadores;

public class Trabalhador : DocumentEntity
{
    [DisplayName("Assinatura do Cliente")]
    public CustomerSubscription? CustomerSubscription { get; set; }

    public string? UserId { get; set; }
}
