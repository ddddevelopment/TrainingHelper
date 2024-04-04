using Results.Domain.Abstractions.Repositories;
using Results.Domain.Models;
using System.Data.SqlClient;

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
            using (SqlConnection connection = new SqlConnection(_dbConfiguration.ConnectionString))
            {
                string addValuesQuery = "INSERT INTO results\r\n" +
                    "VALUES\r\n" +
                    $"('{result.Id}', '{result.Exercise}', {result.WeightKg}, {result.NumberOfRepetitions})";

                using (SqlCommand command = new SqlCommand(addValuesQuery, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}