using MeuPonto.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace MeuPonto;

public class MyClaimsTransformation : IClaimsTransformation
{
    private readonly MeuPontoDbContext _db;

    public MyClaimsTransformation(MeuPontoDbContext db)
    {
        _db = db;
    }


    public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        var trabalhador = await _db.Trabalhadores.FirstOrDefaultAsync(m => m.UserId == principal.GetUserId());

        ClaimsIdentity claimsIdentity = new ClaimsIdentity();

        if (trabalhador == null)
        {

        }
        else
        {
            var claimType = "SubscriptionPlanId";

            if (!principal.HasClaim(claim => claim.Type == claimType))
            {
                var subscriptionPlanId = trabalhador.CustomerSubscription?.SubscriptionPlanId;

                if (subscriptionPlanId.HasValue)
                {
                    claimsIdentity.AddClaim(new Claim(claimType, subscriptionPlanId.ToString()));
                }
            }
        }

        principal.AddIdentity(claimsIdentity);

        return principal;
    }
}