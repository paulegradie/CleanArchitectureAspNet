using Client.Home;
using IntegrationTests.Base;
using Sailfish.Attributes;

namespace Performance;

[Sailfish]
public class UserValidations : IntegrationTest
{
    [SailfishMethod]
    public async Task CreateUserWithManualValidation(CancellationToken ct)
        => await Client.Home.PingHome(new HomeRequest(), ct);
}