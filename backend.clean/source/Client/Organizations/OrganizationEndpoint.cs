using Client.Contracts.Organizations;

namespace Client.Organizations;

public class OrganizationEndpoint : EndpointBase
{
    public OrganizationEndpoint(HttpClient client) : base(client)
    {
    }

    public async Task<GetAllOrganizationUsersResponse> GetAllUsers(GetAllOrganizationsRequest getAllOrganizationsRequest, CancellationToken cancellationToken)
        => await Post<GetAllOrganizationsRequest, GetAllOrganizationUsersResponse>(getAllOrganizationsRequest, cancellationToken);

    public async Task<CreateOrganizationResponse> CreateOrganization(CreateOrganizationRequest createOrganizationRequest, CancellationToken cancellationToken)
        => await Post<CreateOrganizationRequest, CreateOrganizationResponse>(createOrganizationRequest, cancellationToken);
}