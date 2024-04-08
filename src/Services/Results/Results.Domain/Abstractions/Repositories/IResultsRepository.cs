using Results.Domain.Models;

namespace Results.Domain.Abstractions.Repositories
{
    public interface IResultsRepository
    {
        Task Add(Result result);
        Task<Result> Get(Guid id);
        Task Remove(Guid id);
    }
}
