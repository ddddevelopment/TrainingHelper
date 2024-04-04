namespace Results.DAL
{
    public class DbConfiguration
    {
        public DbConfiguration(string connectionString, string databaseName)
        {
            ConnectionString = connectionString;
            DatabaseName = databaseName;
        }

        public string ConnectionString { get; }
        public string DatabaseName { get; }
    }
}
