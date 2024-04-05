using Results.Domain.Abstractions.Repositories;
using Results.Domain.Models;
using Npgsql;

namespace Results.DAL.Repositories
{
    public class ResultsRepository : IResultsRepository
    {
        private readonly DbConfiguration _dbConfiguration;

        public ResultsRepository(DbConfiguration dbConfiguration)
        {
            _dbConfiguration = dbConfiguration;
        }

        public async Task Add(Result result)
        {
            NpgsqlConnectionStringBuilder builder = new NpgsqlConnectionStringBuilder()
            {
                Host = _dbConfiguration.Host,
                Database = _dbConfiguration.Database,
                Username = _dbConfiguration.Username,
                Password = _dbConfiguration.Password
            };

            using (NpgsqlConnection connection = new NpgsqlConnection(builder.ConnectionString))
            {
                string addValuesQuery = "INSERT INTO results\r\n" +
                    "VALUES\r\n" +
                    $"('{result.Id}', '{result.Exercise}', {result.WeightKg}, {result.NumberOfRepetitions})";

                using (NpgsqlCommand command = new NpgsqlCommand(addValuesQuery, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}