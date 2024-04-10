using Users.Domain.Models;

namespace Users.Domain.Abstractions.Repositories
{
    public interface IUsersRepository
    {
        Task Add(User user);
    }
}
