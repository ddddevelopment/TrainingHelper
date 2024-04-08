using Results.Domain.Abstractions.Repositories;
using Results.Domain.Abstractions.Services;
using Results.Domain.Models;

namespace Results.Application.Services
{
    public class ResultsService : IResultsService
    {
        private readonly IResultsRepository _repository;

        public ResultsService(IResultsRepository resultsRepository)
        {
            _repository = resultsRepository;
        }

        public async Task Create(Result result)
        {
            await _repository.Add(result);
        }

        public async Task<Result> Get(Guid id)
        {
            Result result = await _repository.Get(id);

            return result;
        }

        public async Task Delete(Guid id)
        {
            await _repository.Remove(id);
        }
    }
}