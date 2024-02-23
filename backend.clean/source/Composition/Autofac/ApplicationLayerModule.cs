using ApplicationLayer.Abstractions;
using Autofac;

namespace Composition.Autofac;

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