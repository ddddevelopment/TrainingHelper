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
            NpgsqlConnectionStringBuilder builder = CreateConnectionStringBuilderWithProperties();

            using (NpgsqlConnection connection = new NpgsqlConnection(builder.ConnectionString))
            {
                ResultEntity resultEntity = _mapper.Map<ResultEntity>(result);

                string addValuesQuery =
                    "INSERT INTO results\r\n" +
                    "VALUES\r\n" +
                    $"('{resultEntity.Id}', '{resultEntity.Exercise}', {resultEntity.WeightKg}, {resultEntity.NumberOfRepetitions})";

                using (NpgsqlCommand command = new NpgsqlCommand(addValuesQuery, connection))
                {
                    connection.Open();
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<Result> Get(Guid id)
        {
            NpgsqlConnectionStringBuilder builder = CreateConnectionStringBuilderWithProperties();

            using (NpgsqlConnection connection = new NpgsqlConnection(builder.ConnectionString))
            {
                string getQuery =
                "SELECT *\r\n" +
                "FROM results\r\n" +
                $"WHERE id = '{id}'";

                using (NpgsqlCommand command = new NpgsqlCommand(getQuery, connection))
                {
                    connection.Open();

                    using (NpgsqlDataReader resultReader = await command.ExecuteReaderAsync())
                    {
                        while (await resultReader.ReadAsync())
                        {
                            string resultId = resultReader["id"].ToString();
                            string resultExercise = resultReader["exercise"].ToString();
                            string resultWeightkg = resultReader["weight_kg"].ToString();
                            string resultNumberOfRepetitions = resultReader["number_of_Repetitions"].ToString();

                            ResultEntity resultEntity =
                                new ResultEntity(Guid.Parse(resultId), resultExercise, float.Parse(resultWeightkg), int.Parse(resultNumberOfRepetitions));

                            Result result = _mapper.Map<Result>(resultEntity);

                            return result;
                        }

                        throw new Exception($"Result with id = {id} not found");
                    }
                }
            }
        }

        public async Task Remove(Guid id)
        {
            NpgsqlConnectionStringBuilder builder = CreateConnectionStringBuilderWithProperties();

            using (NpgsqlConnection connection = new NpgsqlConnection(builder.ConnectionString))
            {
                string deleteQuery =
                    "DELETE\r\n" +
                    "FROM results\r\n" +
                    $"WHERE id = '{id}'";

                using (NpgsqlCommand command = new NpgsqlCommand(deleteQuery, connection))
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
                Password = _dbConfiguration.Password
            };

            return builder;
        }
    }
}