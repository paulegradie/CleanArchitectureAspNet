using Api.Controllers;
using Client.Organizations;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Features.Organizations;

public class OrganizationsController : AdminOnlyBaseController
{
    private readonly IMediator mediator;


    public OrganizationsController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpPost(CreateOrganizationRequest.ActionRoute)]
    public async Task<CreateOrganizationResponse> CreateOrganization(CreateOrganizationRequest createOrganizationRequest, CancellationToken cancellationToken)
        => await mediator.Send(createOrganizationRequest, cancellationToken);

    [HttpGet(GetAllOrganizationsRequest.ActionRoute)]
    public async Task<GetAllOrganizationUsersResponse> GetAllOrganizations(CancellationToken cancellationToken)
        => await mediator.Send(new GetAllOrganizationsRequest(), cancellationToken);
}