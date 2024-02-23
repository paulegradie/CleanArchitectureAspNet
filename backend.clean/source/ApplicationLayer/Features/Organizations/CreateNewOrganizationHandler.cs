using ApplicationLayer.Contracts.Requests;
using ApplicationLayer.Contracts.Responses;
using Domain.Abstractions.Repositories;
using MediatR;

namespace ApplicationLayer.Features.Organizations;

internal class CreateNewOrganizationHandler : IRequestHandler<CreateAppOrganizationRequest, CreateAppOrganizationResponse>
{
    private readonly IOrganizationsRepository organizationsRepository;

    public CreateNewOrganizationHandler(IOrganizationsRepository organizationsRepository)
    {
        this.organizationsRepository = organizationsRepository;
    }

    public async Task<CreateAppOrganizationResponse> Handle(
        CreateAppOrganizationRequest request,
        CancellationToken cancellationToken)
    {
        var name = request.Name;
        var org = await organizationsRepository.AddOrganization(name, cancellationToken);
        return new CreateAppOrganizationResponse(org.Name);
    }
}