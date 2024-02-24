using Client.Contracts.User;
using IntegrationTests.Utils;
using Sailfish.Attributes;
using Shouldly;

namespace Performance;

[Sailfish(SampleSize = 8)]
public class RegisterUserPerformanceTest : PerformanceTestBase
{
    private string userName = null!;
    private RegisterResponse response = null!;

    [SailfishMethodSetup]
    public async Task CreateUserName()
    {
        userName = Some.RandomUserName();
    }


    [SailfishMethod]
    public async Task Register()
    {
        response = await Client.Users.Register(new RegisterRequest(userName, "Qwerty_123"), CancellationToken);
    }

    [SailfishIterationTeardown]
    public void Check()
    {
        response.UserName.ShouldBe(userName);
        userName = Some.RandomUserName();
    }
}