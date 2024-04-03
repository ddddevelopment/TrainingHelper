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
    }
}
