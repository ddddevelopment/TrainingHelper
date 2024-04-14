using AutoMapper;
using Npgsql;
using Users.DAL.Models;
using Users.Domain.Abstractions.Repositories;
using Users.Domain.Models;

namespace Users.DAL.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly DbConfiguration _dbConfiguration;
        private readonly IMapper _mapper;

        public UsersRepository(DbConfiguration dbConfiguration, IMapper mapper)
        {
            _dbConfiguration = dbConfiguration;
            _mapper = mapper;
        }

        public async Task Add(User user)
        {
            NpgsqlConnectionStringBuilder builder = CreateConnectionStringBuilderWithProperties();

            using (NpgsqlConnection connection = new NpgsqlConnection(builder.ConnectionString))
            {
                UserEntity userEntity = _mapper.Map<UserEntity>(user);

                string addQuery =
                    $"INSERT INTO users\r\n" +
                    "VALUES\r\n" +
                    $"('{userEntity.Id}', '{userEntity.Name}', '{userEntity.Email}')";

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

                        UserEntity userEntity = new UserEntity(id, userName, email);

                        User user = _mapper.Map<User>(userEntity);

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
