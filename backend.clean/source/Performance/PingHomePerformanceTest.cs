using Client.Contracts.Home;
using Sailfish.Attributes;

namespace Performance;

[Sailfish]
public class PingHomePerformance : PerformanceTestBase
{
    [SailfishMethod]
    public async Task CreateUserWithManualValidation(CancellationToken ct)
    {
        await Client.Home.PingHome(new HomeRequest(), ct);
    }
}