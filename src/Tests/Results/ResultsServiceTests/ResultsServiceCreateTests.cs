using Moq;
using Results.Application.Services;
using Results.Domain.Abstractions.Repositories;
using Results.Domain.Models;

namespace ResultsServiceTests
{
    public class ResultsServiceCreateTests
    {
        private Result _result;
        private Mock<IResultsRepository> _repositoryMock;
        private ResultsService _service;

        [SetUp]
        public void Setup()
        {
            _result = new Result();
            _repositoryMock = new Mock<IResultsRepository>();
            _service = new ResultsService(_repositoryMock.Object);
        }

        [Test]
        public async Task Create_ShouldAddResult()
        {
            //arrange

            //act
            await _service.Create(_result);

            //assert
            _repositoryMock.Verify(x => x.Add(_result), Times.Once);
        }
    }
}