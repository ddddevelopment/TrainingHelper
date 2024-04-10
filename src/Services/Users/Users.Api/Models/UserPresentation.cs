namespace Users.Api.Models
{
    public class UserPresentation
    {
        public UserPresentation(int id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;
        }

        public int Id { get; }
        public string Name { get; }
        public string Email { get; }
    }
}
