namespace Client.Home;

public record HomeRequest : RequestBase
{
    public const string ActionRoute = "api";
    public override string GetActionRoute() => ActionRoute;
}