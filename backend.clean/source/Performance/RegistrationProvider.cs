using Autofac;
using IntegrationTests.Base;
using Sailfish.Registration;

namespace Performance;

public class RegistrationProvider : IProvideARegistrationCallback
{
    public async Task RegisterAsync(ContainerBuilder builder, CancellationToken cancellationToken = new CancellationToken())
    {
        // var server = new Server();
        // builder.RegisterInstance(server);
        await Task.Yield();
    }
}