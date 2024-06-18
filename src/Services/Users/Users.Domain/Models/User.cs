namespace Users.Domain.Models
{
    public class User
    {
        public User(Guid id, string name, string email, string passwordHash)
        {
            Id = id;
            Name = name;
            Email = email;
            PasswordHash = passwordHash;
        }

        public Guid Id { get; }
        public string Name { get; }
        public string Email { get; }
        public string PasswordHash { get; }
    }
}
