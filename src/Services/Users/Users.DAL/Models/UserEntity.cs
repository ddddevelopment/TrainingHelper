namespace Users.DAL.Models
{
    public class UserEntity
    {
        public UserEntity(Guid id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;
        }

        public Guid Id { get; }
        public string Name { get; }
        public string Email { get; }
    }
}
