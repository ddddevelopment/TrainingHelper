using Users.Domain.Models;

namespace Users.Domain.Abstractions.Services
{
    public interface IUsersService
    {
        Task Create(User user);
        Task<User> Get(Guid id);
        Task Delete(Guid id);
    }
}
