using Domain.Models;

namespace Domain.Repositories;

public interface IAuthenticationRepository : IRepository<User>
{
    Task Authenticate();
    
}