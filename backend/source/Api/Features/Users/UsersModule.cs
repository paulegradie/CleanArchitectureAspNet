using Autofac;

namespace Api.Features.Users;

public class UsersModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);
        builder.RegisterType<UserRetriever>().As<IUserRetriever>().InstancePerLifetimeScope();
    }
}