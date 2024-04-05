using Results.Domain.Abstractions.Repositories;
using Results.Domain.Models;
using Npgsql;
using AutoMapper;
using Results.DAL.Entities;

namespace Results.DAL.Repositories
{
    public class ResultsRepository : IResultsRepository
    {
        private readonly DbConfiguration _dbConfiguration;
        private readonly IMapper _mapper;

        public ResultsRepository(DbConfiguration dbConfiguration, IMapper mapper)
        {
            _dbConfiguration = dbConfiguration;
            _mapper = mapper;
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
                ResultEntity resultEntity = _mapper.Map<ResultEntity>(result);

                string addValuesQuery = "INSERT INTO results\r\n" +
                    "VALUES\r\n" +
                    $"('{resultEntity.Id}', '{resultEntity.Exercise}', {resultEntity.WeightKg}, {resultEntity.NumberOfRepetitions})";

                using (NpgsqlCommand command = new NpgsqlCommand(addValuesQuery, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}