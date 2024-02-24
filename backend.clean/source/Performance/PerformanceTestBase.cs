using IntegrationTests.Base;
using Sailfish.Attributes;

namespace Performance;

public abstract class PerformanceTestBase : IntegrationTest
{
    [SailfishGlobalSetup]
    public override Task InitializeAsync()
    {
        return base.InitializeAsync();
    }

    [SailfishGlobalTeardown]
    public override Task DisposeAsync()
    {
        return base.DisposeAsync();
    }
}