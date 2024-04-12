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
                    $"INSERT INTO users\r\n" +
                    "VALUES\r\n" +
                    $"('{user.Id}', '{user.Name}', '{user.Email}')";

                using (NpgsqlCommand command = new NpgsqlCommand(addQuery, connection))
                {
                    connection.Open();
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<User> Get(Guid id)
        {
            NpgsqlConnectionStringBuilder builder = CreateConnectionStringBuilderWithProperties();

            using (NpgsqlConnection connection = new NpgsqlConnection(builder.ConnectionString))
            {
                string getQuery =
                    "SELECT *\r\n" +
                    "FROM users\r\n" +
                    $"WHERE user_id = '{id}'";
                using (NpgsqlCommand command = new NpgsqlCommand(getQuery, connection))
                {
                    connection.Open();
                    NpgsqlDataReader reader = command.ExecuteReader();

                    while (await reader.ReadAsync())
                    {
                        string userName = reader["user_name"].ToString();
                        string email = reader["email"].ToString();

                        User user = new User(id, userName, email);

                        return user;
                    }

                    throw new Exception($"User with id = {id} not found");
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
