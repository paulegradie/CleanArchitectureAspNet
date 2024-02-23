using Autofac;
using Domain.Abstractions;

namespace Composition.Autofac;

public class DomainModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        base.Load(builder);
        builder
            .RegisterAssemblyTypes(assemblies)
            .AsClosedTypesOf(typeof(IMapToTheDomain<,>))
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();
    }
}