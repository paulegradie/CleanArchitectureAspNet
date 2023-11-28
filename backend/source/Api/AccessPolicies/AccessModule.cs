using Autofac;

namespace Api.AccessPolicies;

public class AccessModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);
        builder.RegisterType<Jwt>().As<IJwt>().SingleInstance();
    }
}