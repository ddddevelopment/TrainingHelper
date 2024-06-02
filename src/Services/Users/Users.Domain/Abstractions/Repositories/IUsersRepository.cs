using Users.Domain.Models;

namespace Users.Domain.Abstractions.Repositories
{
    public interface IUsersRepository
    {
        Task Add(User user);
        Task<User> Get(Guid id);
        Task<User> Get(string email);
        Task Remove(Guid id);
    }
}
