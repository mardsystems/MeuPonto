using MeuPonto.Enums;
using MeuPonto.Models;

namespace MeuPonto.Factories;

public static class TrabalhadorFactory
{
    public static Trabalhador CriaTrabalhador(TransactionContext transaction)
    {
        var trabalhador = new Trabalhador
        {
            CustomerSubscription = new CustomerSubscription
            {
                SubscriptionPlanId = SubscriptionPlanEnum.Bronze
            },
            UserId = transaction.UserId,
            CreationDate = transaction.DateTime
        };

        return trabalhador;
    }

    public static void RecontextualizaTrabalhador(this Trabalhador trabalhador, TransactionContext transaction)
    {
        trabalhador.UserId = transaction.UserId;
        trabalhador.CreationDate ??= transaction.DateTime;
    }
}
