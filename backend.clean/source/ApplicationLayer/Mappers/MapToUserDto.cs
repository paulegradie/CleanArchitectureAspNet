using ApplicationLayer.Abstractions;
using Client.Contracts.User;
using Domain.Models;

namespace ApplicationLayer.Mappers;

internal class MapToUserDto : IMapToExternalDto<User, UserDto>
{
    public Task<UserDto> Map(User from, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(new UserDto(from.Name, from.IsAdmin));
    }
}