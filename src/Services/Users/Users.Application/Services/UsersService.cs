using Users.Domain.Abstractions.Repositories;
using Users.Domain.Abstractions.Services;
using Users.Domain.Models;

namespace Users.Application.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _repository;

        public UsersService(IUsersRepository repository)
        {
            _repository = repository;
        }

        public async Task Create(User user)
        {
            await _repository.Add(user);
        }

        public async Task<User> Get(Guid id)
        {
            User user = await _repository.Get(id);
            return user;
        }

        public async Task Delete(Guid id)
        {
            await _repository.Remove(id);
        }
    }
}
