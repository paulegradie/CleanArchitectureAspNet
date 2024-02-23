using Domain.Models;

namespace Domain.Abstractions.Repositories;

public interface IUserRepository
{
    Task<User> GetAdminUser(CancellationToken cancellationToken);
}