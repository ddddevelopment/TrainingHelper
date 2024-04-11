using Moq;
using Users.Application.Services;
using Users.Domain.Abstractions.Repositories;
using Users.Domain.Abstractions.Services;
using Users.Domain.Models;

namespace UsersServiceTests
{
    public class UsersServiceGetTests
    {
        private Mock<IUsersRepository> _repositoryMock;
        private IUsersService _service;

        [SetUp]
        public void Setup()
        {
            _repositoryMock = new Mock<IUsersRepository>();
            _service = new UsersService(_repositoryMock.Object);
        }

        [Test]
        public async Task Get_ShouldReturnUser()
        {
            //arrange
            Guid id = Guid.NewGuid();
            _repositoryMock.Setup(x => x.Get(id)).ReturnsAsync(new User(id, "fakfja", "kfjafha"));

            //act
            User user = await _service.Get(id);

            //assert
            _repositoryMock.Verify(x => x.Get(id), Times.Once);
            Assert.NotNull(user);
        }
    }
}
