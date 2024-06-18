namespace Auth.Domain.Models
{
    public class UserLogin
    {
        public UserLogin(string email, string passwordHash)
        {
            Email = email;
            PasswordHash = passwordHash;
        }

        public string Email { get; }
        public string PasswordHash { get; }
    }
}
