using System.ComponentModel;

namespace MeuPonto.Models;

public class Trabalhador : LocalTableEntity
{
    [DisplayName("Assinatura do Cliente")]
    public CustomerSubscription? CustomerSubscription { get; set; }

    public string? UserId { get; set; }
}
