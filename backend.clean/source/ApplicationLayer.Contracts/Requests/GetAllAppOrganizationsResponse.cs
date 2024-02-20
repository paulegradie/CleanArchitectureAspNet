using Client.Contracts.Organizations;

namespace ApplicationLayer.Contracts.Requests;

public record GetAllAppOrganizationsResponse(IEnumerable<OrganizationDto> OrganizationDtos);