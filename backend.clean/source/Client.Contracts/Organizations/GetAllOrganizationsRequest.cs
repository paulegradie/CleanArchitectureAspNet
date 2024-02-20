namespace Client.Contracts.Organizations;

public record GetAllOrganizationsRequest : RequestBase
{
    public const string ActionRoute = "api/organizations/all-users";
    public override string GetActionRoute() => ActionRoute;
}