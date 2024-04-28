namespace Auth.Api.Models
{
    public class UserLoginRequest
    {
        public UserLoginRequest(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public string Email { get; }
        public string Password { get; }
    }
}
