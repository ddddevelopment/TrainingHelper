using Moq;
using Results.Application.Services;
using Results.Domain.Abstractions.Repositories;

namespace ResultsServiceTests
{
    public class ResultsServiceDeleteTests
    {
        private ResultsService _service;
        private Mock<IResultsRepository> _repositoryMock;

        [SetUp]
        public async Task Setup()
        {
            _repositoryMock = new Mock<IResultsRepository>();
            _service = new ResultsService(_repositoryMock.Object);
        }

        [Test]
        public async Task Delete_ShouldRemoveResult()
        {
            //arrange
            Guid id = Guid.NewGuid();

            //act
            await _service.Delete(id);

            //assert
            _repositoryMock.Verify(x => x.Remove(id), Times.Once);  
        }
    }
}
