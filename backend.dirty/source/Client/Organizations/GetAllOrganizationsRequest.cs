using MediatR;

namespace Client.Organizations;

public record GetAllOrganizationsRequest : RequestBase, IRequest<GetAllOrganizationUsersResponse>
{
    public const string ActionRoute = "api/organizations/all-users";
    public override string GetActionRoute() => ActionRoute;
}