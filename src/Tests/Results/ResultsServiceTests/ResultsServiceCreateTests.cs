using Moq;
using Results.Application.Services;
using Results.Domain.Abstractions.Repositories;
using Results.Domain.Models;

namespace ResultsServiceTests
{
    public class ResultsServiceCreateTests
    {
        private Mock<IResultsRepository> _repositoryMock;
        private ResultsService _service;

        [SetUp]
        public void Setup()
        {
            _repositoryMock = new Mock<IResultsRepository>();
            _service = new ResultsService(_repositoryMock.Object);
        }

        [Test]
        public async Task Create_ShouldAddResult()
        {
            //arrange
            string exercise = "bench press";
            float weightKg = 120;
            int numberOfRepetitions = 3;
            Result result = new Result(exercise, weightKg, numberOfRepetitions);

            //act
            await _service.Create(result);

            //assert
            _repositoryMock.Verify(x => x.Add(result), Times.Once);
        }
    }
}