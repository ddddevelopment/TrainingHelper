namespace Users.Domain.Models
{
    public class User
    {
        public User(string name, string email)
        {
            Name = name;
            Email = email;
        }

        public Guid Id { get; } = Guid.NewGuid();
        public string Name { get; }
        public string Email { get; }
    }
}
