using Users.Domain.Models;

namespace Users.Domain.Abstractions.Services
{
    public interface IUsersService
    {
        Task Create(User user);
    }
}
