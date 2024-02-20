using Domain.Models;

namespace Domain.Repositories;

public interface IUserRepository
{
    Task<User> GetAdminUser(CancellationToken cancellationToken);
}