﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore;

namespace Billing.Models;

[Owned]
public class CustomerSubscription
{
    [Required]
    [DisplayName("Plano de Assinatura")]
    public SubscriptionPlanEnum? SubscriptionPlanId { get; set; }
}
