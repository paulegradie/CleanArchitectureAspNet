using Autofac;
using Domain.Abstractions;

namespace Composition.Autofac;

public class DomainModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        builder
            .RegisterAssemblyTypes(assemblies)
            .AsClosedTypesOf(typeof(IMapToTheDomain<,>))
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();
    }
}