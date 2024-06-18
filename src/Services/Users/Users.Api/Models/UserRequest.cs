namespace Users.Api.Models
{
    public class UserRequest
    {
        public UserRequest(string name, string email, string password)
        {
            Name = name;
            Email = email;
            Password = password;
        }

        public string Name { get; }
        public string Email { get; }
        public string Password { get; }
    }
}
