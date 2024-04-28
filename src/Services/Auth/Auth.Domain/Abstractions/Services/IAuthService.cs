using Auth.Domain.Models;

namespace Auth.Domain.Abstractions.Services
{
    public interface IAuthService
    {
        Task<string> Login(UserLogin userLogin);
    }
}
