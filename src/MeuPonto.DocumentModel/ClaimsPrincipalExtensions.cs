using System.Security.Claims;

namespace MeuPonto;

public static class ClaimsPrincipalExtensions
{
    public static TransactionContext CreateTransaction(this ClaimsPrincipal user)
    {
        var nameIdentifier = user.FindFirst(ClaimTypes.NameIdentifier);

        var userId = Guid.Parse(nameIdentifier.Value);

        var transaction = new TransactionContext(userId);

        return transaction;
    }
}
