using Users.Domain.Models;

namespace Users.Domain.Abstractions.Repositories
{
    public interface IUsersRepository
    {
        Task Add(User user);
        Task<User> Get(Guid id);
        Task Remove(Guid id);
    }
}
