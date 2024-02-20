namespace Client.Contracts.Organizations;

public record CreateOrganizationRequest(string Name, List<Guid> UserIds) : RequestBase
{
    public const string ActionRoute = "api/organizations/create";
    public override string GetActionRoute() => ActionRoute;
}