using Users.Domain.Abstractions.Repositories;
using Users.Domain.Models;

namespace Users.DAL.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        public Task Add(User user)
        {
            throw new NotImplementedException();
        }
    }
}
