using System.Reflection;
using ApplicationLayer.Abstractions;
using Autofac;
using MediatR;
using MediatR.Extensions.FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Module = Autofac.Module;

namespace ApplicationLayer.Registrations;

public static class ApplicationLayerRegistrationExtensionMethods
{
    public static void RegisterApplicationLayerHandlers(
        this IServiceCollection serviceCollection,
        Assembly[] assemblies)
    {
        serviceCollection.AddMediatR(configuration =>
        {
            foreach (var assembly in assemblies)
            {
                configuration.RegisterServicesFromAssembly(assembly);
            }
        });
        serviceCollection.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        serviceCollection.AddFluentValidation(assemblies);
    }
}

public class ApplicationLayerModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        base.Load(builder);
        builder
            .RegisterAssemblyTypes(assemblies)
            .AsClosedTypesOf(typeof(IMapToExternalDto<,>))
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();
    }
}