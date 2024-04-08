using Results.Domain.Models;

namespace Results.Domain.Abstractions.Services
{
    public interface IResultsService
    {
        Task Create(Result result);
        Task<Result> Get(Guid id);
        Task Delete(Guid id);
    }
}
