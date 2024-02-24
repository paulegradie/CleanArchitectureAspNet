using ApplicationLayer;
using Autofac;
using Composition.Autofac;
using Composition.ServiceCollection;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Composition;

public static class CompositionUtility
{
    public static void RegisterServiceProviders(this IServiceCollection serviceCollection)
    {
        serviceCollection.RegisterApplicationLayerHandlers();
        serviceCollection.ConfigureDatabaseServices();
        serviceCollection.ConfigureAuthentication();
    }

    public static void ConfigureRegistrations(this ConfigureHostBuilder builder)
    {
        builder.ConfigureContainer<ContainerBuilder>(containerBuilder =>
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            containerBuilder.RegisterAssemblyTypes(assemblies)
                .Where(t => t.IsClosedTypeOf(typeof(IValidator<>)))
                .AsImplementedInterfaces();

            containerBuilder.RegisterModule<ApplicationLayerModule>();
            containerBuilder.RegisterModule<AuthenticationModule>();
            containerBuilder.RegisterModule<DomainModule>();
            containerBuilder.RegisterModule<PersistenceModule>();
        });
    }
}