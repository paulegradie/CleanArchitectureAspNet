using Autofac;

namespace Api.Domain;

public class DomainModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);
        builder.RegisterType<EntityConventionApplier>().As<IEntityConventionApplier>().SingleInstance();
        builder.RegisterAssemblyTypes(ThisAssembly).As<IEntityPropertyConvention>().SingleInstance();
    }
}