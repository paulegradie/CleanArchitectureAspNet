
using Domain.Models;

namespace Domain.Repositories;

public interface IOrganizationsRepository
{
    Task<Organization> AddOrganization(string name, CancellationToken cancellationToken);
    Task<IEnumerable<Organization>> GetAllOrganizations(CancellationToken cancellationToken);
}

