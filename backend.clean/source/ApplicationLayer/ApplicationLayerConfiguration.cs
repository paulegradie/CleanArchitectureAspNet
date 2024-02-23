using MediatR;
using MediatR.Extensions.FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace ApplicationLayer;

public static class ApplicationLayerConfiguration
{
    public static void RegisterApplicationLayerHandlers(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddMediatR(configuration =>
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                configuration.RegisterServicesFromAssembly(assembly);
            }
        });
        serviceCollection.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        serviceCollection.AddFluentValidation(AppDomain.CurrentDomain.GetAssemblies());
    }
}