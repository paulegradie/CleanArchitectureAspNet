using Authentication;
using Authentication.Abstractions;
using Authentication.Abstractions.AccessPolicies;
using Authentication.AccessPolicies;
using Autofac;

namespace Composition;

public class AuthenticationModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);
        builder.RegisterType<Jwt>().As<IJwt>().SingleInstance();
        builder.RegisterType<Authenticator>().As<IAuthenticator>();
    }
}