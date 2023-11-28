using MediatR;

namespace Client.Organizations;

public record CreateOrganizationRequest(string Name, List<Guid> UserIds) : RequestBase, IRequest<CreateOrganizationResponse>
{
    public const string ActionRoute = "api/organizations/create";
    public override string GetActionRoute() => ActionRoute;
}