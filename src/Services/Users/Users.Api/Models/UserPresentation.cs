namespace Users.Api.Models
{
    public class UserPresentation
    {
        public UserPresentation(string name, string email)
        {
            Name = name;
            Email = email;
        }

        public string Name { get; }
        public string Email { get; }
    }
}
