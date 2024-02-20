using Authentication.Abstractions;
using Client;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Persistence;

namespace IntegrationTests.Base;

public class IntegrationTest : IAsyncLifetime
{
    private IDbContextTransaction transaction = null!;

    protected IntegrationTest()
    {
        Server = new Server(); // if we want to share the server within a class, use IClassFixture approach instead
        Logger = Server.Services.GetRequiredService<ILogger<IntegrationTest>>();

        var client = Server.CreateClient();
        Client = new ServerClient(client);
        CancellationToken = new CancellationTokenSource(TimeSpan.FromMinutes(5)).Token;
    }

    private Server Server { get; set; }

    protected ILogger Logger { get; set; }

    protected ServerClient Client { get; set; }

    protected CancellationToken CancellationToken { get; set; }

    public async Task InitializeAsync()
    {
        // var db = await ConfigureAndSeedDatabase();
        var db = Server.Services.GetRequiredService<AppDbContext>();
        // await db.Database.EnsureDeletedAsync(CancellationToken);
        await db.Database.EnsureCreatedAsync(CancellationToken);
        await Server.Services.EnsureDefaultRoles();
        transaction = await db.Database.BeginTransactionAsync(CancellationToken);
    }

    // private async Task<AppDbContext> ConfigureAndSeedDatabase()
    // {
    //     var db = Server.Services.GetRequiredService<AppDbContext>();
    //     // await db.Database.EnsureDeletedAsync(CancellationToken);
    //     await db.Database.EnsureCreatedAsync(CancellationToken);
    //     await Server.Services.EnsureDefaultRoles();
    //     return db;
    // }

    public async Task DisposeAsync()
    {
        if (transaction is not null)
        {
            await transaction.RollbackAsync(CancellationToken);
            await transaction.DisposeAsync();
        }
        // await Server.DisposeAsync();
    }
}