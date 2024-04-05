namespace Results.DAL
{
    public class DbConfiguration
    {
        public DbConfiguration(string host, string database, string username, string password)
        {
            Host = host;
            Database = database;
            Username = username;
            Password = password;
        }

        public string Host { get; }
        public string Database { get; }
        public string Username { get; }
        public string Password { get; }
    }
}
