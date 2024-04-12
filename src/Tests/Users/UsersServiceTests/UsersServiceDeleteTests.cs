using Moq;
using Users.Application.Services;
using Users.Domain.Abstractions.Repositories;

namespace UsersServiceTests
{
    public class UsersServiceDeleteTests
    {
        private UsersService _service;
        private Mock<IUsersRepository> _repositoryMock;

        [SetUp]
        public async Task Setup()
        {
            _repositoryMock = new Mock<IUsersRepository>();
            _service = new UsersService(_repositoryMock.Object);
        }

        [Test]
        public async Task Delete_ShouldRemoveUser()
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
