namespace MeuPonto.Infrastructure;

public static class InfrastructureModule
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddDbServices();

        //

        return services;
    }
}
