namespace Users.Api.Models
{
    public class UserRequest
    {
        public UserRequest(string name, string email)
        {
            Name = name;
            Email = email;
        }

        public string Name { get; }
        public string Email { get; }
    }
}
