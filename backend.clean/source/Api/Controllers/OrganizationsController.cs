using Api.Controllers.Bases;
using ApplicationLayer.Contracts.Requests;
using Client.Contracts.Organizations;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class OrganizationsController : AdminOnlyBaseController
{
    private readonly IMediator mediator;


    public OrganizationsController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpPost(CreateOrganizationRequest.ActionRoute)]
    public async Task<CreateOrganizationResponse> CreateOrganization(
        CreateOrganizationRequest createOrganizationRequest, CancellationToken cancellationToken)
    {
        var response = await mediator.Send(
            new CreateAppOrganizationRequest(createOrganizationRequest.Name),
            cancellationToken);
        return new CreateOrganizationResponse(response.Name);
    }

    [HttpGet(GetAllOrganizationsRequest.ActionRoute)]
    public async Task<GetAllOrganizationUsersResponse> GetAllOrganizations(CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new GetAllAppOrganizationsRequest(), cancellationToken);
        return new GetAllOrganizationUsersResponse(response.OrganizationDtos);
    }
}