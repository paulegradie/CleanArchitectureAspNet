using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace IntegrationTests.Base;

public class CustomServiceProviderFactory : IServiceProviderFactory<ContainerBuilder>
{
    private readonly AutofacServiceProviderFactory wrapped;
    private IServiceCollection? services;

    public CustomServiceProviderFactory()
    {
        wrapped = new AutofacServiceProviderFactory();
    }

    public ContainerBuilder CreateBuilder(IServiceCollection services)
    {
        this.services = services;
        return wrapped.CreateBuilder(services);
    }

    public IServiceProvider CreateServiceProvider(ContainerBuilder containerBuilder)
    {
        if (services is null) throw new InvalidOperationException("Services collection is null");

        var sp = services.BuildServiceProvider();
#pragma warning disable CS0612
        var filters = sp.GetRequiredService<IEnumerable<IStartupConfigureContainerFilter<ContainerBuilder>>>();
#pragma warning restore CS0612

        foreach (var filter in filters)
        {
            filter.ConfigureContainer(b => { })(containerBuilder);
        }

        return wrapped.CreateServiceProvider(containerBuilder);
    }
}