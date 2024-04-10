using Npgsql;
using Users.Domain.Abstractions.Repositories;
using Users.Domain.Models;

namespace Users.DAL.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly DbConfiguration _dbConfiguration;

        public UsersRepository(DbConfiguration dbConfiguration) 
        {
            _dbConfiguration = dbConfiguration;
        }

        public async Task Add(User user)
        {
            NpgsqlConnectionStringBuilder builder = CreateConnectionStringBuilderWithProperties();

            using (NpgsqlConnection connection = new NpgsqlConnection(builder.ConnectionString))
            {
                string addQuery =
                    $"INSERT INTO Results\r\n" +
                    "VALUES\r\n" +
                    $"('{user.Id}', '{user.Name}', '{user.Email}')";

                using (NpgsqlCommand command = new NpgsqlCommand(addQuery, connection))
                {
                    connection.Open();
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        private NpgsqlConnectionStringBuilder CreateConnectionStringBuilderWithProperties()
        {
            NpgsqlConnectionStringBuilder builder = new NpgsqlConnectionStringBuilder()
            {
                Host = _dbConfiguration.Host,
                Database = _dbConfiguration.Database,
                Username = _dbConfiguration.Username,
                Password = _dbConfiguration.Password,
            };

            return builder;
        }
    }
}
