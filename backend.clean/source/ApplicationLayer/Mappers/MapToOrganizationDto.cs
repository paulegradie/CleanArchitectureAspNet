using ApplicationLayer.Abstractions;
using Client.Contracts.Organizations;
using Domain.Models;

namespace ApplicationLayer.Mappers;

internal class MapToOrganizationDto : IMapToExternalDto<Organization, OrganizationDto>
{
    public Task<OrganizationDto> Map(Organization from, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(new OrganizationDto(from.Name));
    }
}