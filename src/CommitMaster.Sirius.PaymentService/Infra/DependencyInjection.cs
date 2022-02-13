

using CommitMaster.Sirius.Infra.CrossCutting;

namespace CommitMaster.Sirius.PaymentService.Infra;

public static class DependencyInjection
{
    public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHostedService<Worker>();
        
        services.AddMessageBus(configuration["ConnectionStrings:RabbitMQ"]);
        
        
        return services;
    }
}
