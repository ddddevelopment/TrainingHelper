namespace Users.DAL.Models
{
    public class UserEntity
    {
        public UserEntity(Guid id, string name, string email, string passwordHash)
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
