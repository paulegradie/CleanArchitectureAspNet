using Domain.Models;

namespace Domain.Abstractions.Repositories;

public interface IAuthenticationRepository : IRepository<User>
{
    Task Authenticate();
}