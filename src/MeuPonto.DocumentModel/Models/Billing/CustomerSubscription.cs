using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MeuPonto.Models.Billing;

public class CustomerSubscription
{
    [Required]
    [DisplayName("Plano de Assinatura")]
    public SubscriptionPlanEnum? SubscriptionPlanId { get; set; }
}
