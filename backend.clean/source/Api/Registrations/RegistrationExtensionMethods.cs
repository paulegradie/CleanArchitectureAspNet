using Autofac;
using FluentValidation;

namespace Api.Registrations;

public static class RegistrationExtensionMethods
{
    public static void ConfigureRegistrations(this ConfigureHostBuilder builder)
    {
        builder.ConfigureContainer<ContainerBuilder>(containerBuilder =>
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            containerBuilder.RegisterAssemblyModules(assemblies);
            containerBuilder.RegisterAssemblyTypes(assemblies)
                .Where(t => t.IsClosedTypeOf(typeof(IValidator<>)))
                .AsImplementedInterfaces();
        });
    }
}