using Api.AccessPolicies;
using Api.Domain;
using Client;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace IntegrationTests.Base;

public class IntegrationTest : IAsyncLifetime
{
    protected IntegrationTest()
    {
        Logger = Log.Logger;
        Server = new Server(); // if we want to share the server within a class, use IClassFixture approach instead

        var client = Server.CreateClient();
        Client = new ApiClient(client);
        CancellationToken = new CancellationTokenSource(TimeSpan.FromMinutes(5)).Token;
    }

    private async Task ConfigureAndSeedDatabase()
    {
        var services = Server.Services;
        var db = services.GetService<AppDbContext>();
        await db!.Database.EnsureDeletedAsync(CancellationToken);
        await db.Database.EnsureCreatedAsync(CancellationToken);

        await services.EnsureDefaultRoles();
    }

    private Server Server { get; set; }
    protected ILogger Logger { get; set; }
    protected ApiClient Client { get; set; }
    protected CancellationToken CancellationToken { get; set; }

    public async Task InitializeAsync()
    {
        await ConfigureAndSeedDatabase();
    }

    public async Task DisposeAsync()
    {
        await Server.DisposeAsync();
    }
}