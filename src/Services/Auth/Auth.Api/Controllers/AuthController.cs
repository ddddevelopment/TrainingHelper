using Auth.Api.Models;
using Auth.Domain.Abstractions.Services;
using Auth.Domain.Models;
using AutoMapper;
using MessageBroker.RabbitMq;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;
        private readonly IMapper _mapper;

        public AuthController(IAuthService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpPost("/login")]
        public async Task<ActionResult<string>> Login(UserLoginRequest userLoginRequest)
        {
            UserLogin userLogin = _mapper.Map<UserLogin>(userLoginRequest);

            bool isUserExists = default;

            RpcClient rpcClient = new RpcClient();

            isUserExists = await rpcClient.CallAsync(userLogin.Email);

            if (isUserExists)
            {
                return Ok(await _service.Login(userLogin));
            }

            else
            {
                return BadRequest("User is not exists");
            }
        }
    }
}
