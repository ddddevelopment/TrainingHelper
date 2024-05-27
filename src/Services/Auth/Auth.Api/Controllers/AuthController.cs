using Auth.Api.Models;
using Auth.Domain;
using Auth.Domain.Abstractions.Services;
using Auth.Domain.Models;
using AutoMapper;
using MessageBroker.RabbitMq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Auth.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private const string EXCHANGE_NAME = "training";

        private readonly IAuthService _service;
        private readonly IMapper _mapper;

        public AuthController(IAuthService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;   
        }

        [HttpPost("/login")]
        public async Task<string> Login(UserLoginRequest userLoginRequest)
        {
            UserLogin userLogin = _mapper.Map<UserLogin>(userLoginRequest);

            Publisher publisher = new Publisher(EXCHANGE_NAME);

            await publisher.Publish("users", $"{userLogin.Email}, {userLogin.Password}");

            return await _service.Login(userLogin);
        }
    }
}
