using Moq;
using Results.Application.Services;
using Results.DAL.Repositories;
using Results.Domain.Abstractions.Repositories;
using Results.Domain.Models;

namespace ResultsServiceTests
{
    public class ResultsServiceGetTests
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
        public async Task Get_ShouldRetutnValidResult()
        {
            //arrange
            string expectedExercise = "bench press";
            float expectedWeightKg = 100;
            int expectedNumberOfRepetitions = 2;
            Guid id = Guid.NewGuid();
            _repositoryMock.Setup(repository => repository.Get(id))
                .ReturnsAsync(() => new Result(expectedExercise, expectedWeightKg, expectedNumberOfRepetitions))
                .Verifiable(Times.Once);

            //act
            Result result = await _service.Get(id);

            //assert
            _repositoryMock.VerifyAll();
        }
    }
}
