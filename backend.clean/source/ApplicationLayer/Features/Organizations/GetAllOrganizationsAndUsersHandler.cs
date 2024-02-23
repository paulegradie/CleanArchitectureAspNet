using ApplicationLayer.Abstractions;
using ApplicationLayer.Contracts.Requests;
using Client.Contracts.Organizations;
using Domain.Abstractions.Repositories;
using Domain.Models;
using MediatR;

namespace ApplicationLayer.Features.Organizations;

internal class GetAllOrganizationsAndUsersHandler
    : IRequestHandler<GetAllAppOrganizationsRequest, GetAllAppOrganizationsResponse>
{
    private readonly IOrganizationsRepository organizationsRepository;
    private readonly IMapToExternalDto<Organization, OrganizationDto> toOrganizationDtoMapper;

    public GetAllOrganizationsAndUsersHandler(
        IOrganizationsRepository organizationsRepository,
        IMapToExternalDto<Organization, OrganizationDto> toOrganizationDtoMapper)
    {
        this.organizationsRepository = organizationsRepository;
        this.toOrganizationDtoMapper = toOrganizationDtoMapper;
    }

    public async Task<GetAllAppOrganizationsResponse> Handle(
        GetAllAppOrganizationsRequest request,
        CancellationToken cancellationToken)
    {
        var organizationsDomainModels = await organizationsRepository.GetAllOrganizations(cancellationToken);
        var dtos = new List<OrganizationDto>();
        foreach (var model in organizationsDomainModels)
        {
            var dto = await toOrganizationDtoMapper.Map(model, cancellationToken);
            dtos.Add(dto);
        }

        return new GetAllAppOrganizationsResponse(dtos);
    }
}