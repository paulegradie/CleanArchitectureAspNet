using Client.Contracts.Home;
using IntegrationTests.Base;
using Sailfish.Attributes;

namespace Performance;

[Sailfish]
public class PingHomePerformance : IntegrationTest
{
    [SailfishMethod]
    public async Task CreateUserWithManualValidation(CancellationToken ct)
        => await Client.Home.PingHome(new HomeRequest(), ct);

    [SailfishMethod]
    public async Task TestMethod(CancellationToken ct)
        => await Task.CompletedTask;
}