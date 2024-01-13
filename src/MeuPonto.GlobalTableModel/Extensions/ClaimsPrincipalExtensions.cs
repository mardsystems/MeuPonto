using MeuPonto.Models.Billing;
using System.Security.Claims;

namespace MeuPonto.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static TransactionContext CreateTransaction(this ClaimsPrincipal user)
    {
        var userId = user.GetUserId();

        var transaction = new TransactionContext(userId);

        return transaction;
    }

    public static string GetUserId(this ClaimsPrincipal user)
    {
        var nameIdentifier = user.FindFirst(ClaimTypes.NameIdentifier);

        var userId = nameIdentifier.Value;

        return userId;
    }

    public static SubscriptionPlanEnum GetSubscriptionPlanId(this ClaimsPrincipal user)
    {
        var subscriptionPlanClaim = user.FindFirst("SubscriptionPlanId");

        if (subscriptionPlanClaim == null)
        {
            return SubscriptionPlanEnum.Bronze;
        }
        else
        {
            var subscriptionPlanId = (SubscriptionPlanEnum)Enum.Parse(typeof(SubscriptionPlanEnum), subscriptionPlanClaim.Value);

            return subscriptionPlanId;
        }
    }
}
