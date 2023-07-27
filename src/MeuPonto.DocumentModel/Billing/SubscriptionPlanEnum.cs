using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MeuPonto.Billing;

public enum SubscriptionPlanEnum
{
    [Display(Name = "Bronze")]
    Bronze = 1,

    [Display(Name = "Prata")]
    Silver = 2,

    [Display(Name = "Ouro")]
    Gold = 3
}
