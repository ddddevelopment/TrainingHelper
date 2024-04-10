using Moq;
using NUnit.Framework.Constraints;
using Users.Application.Services;
using Users.Domain.Abstractions.Repositories;
using Users.Domain.Abstractions.Services;
using Users.Domain.Models;

namespace UsersServiceTests
{
    public class UsersServiceCreateTests
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
        public async Task Create_ShouldAddUser()
        {
            //arrange

            //act
            var user = new User(3, "fakj", "fjaf");

            await _service.Create(user);

            //assert
            _repositoryMock.Verify(x => x.Add(user), Times.Once);
        }
    }
}