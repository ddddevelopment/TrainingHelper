using Auth.Api.Models;
using Auth.Domain.Abstractions.Services;
using Auth.Domain.Models;
using Auth.MessageBroker.RabbitMQ;
using AutoMapper;
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
            UserLogin userLogin = new UserLogin(userLoginRequest.Email, BCrypt.Net.BCrypt.HashPassword(userLoginRequest.Password));

            bool isUserExists = default;

            RpcClient rpcClient = new RpcClient();

            UserLogin receivedUserLogin = await rpcClient.CallAsync(userLogin.Email);

            isUserExists = receivedUserLogin != null;

            if (isUserExists)
            {
                bool isPasswordCorrect = BCrypt.Net.BCrypt.Verify(userLoginRequest.Password, receivedUserLogin.PasswordHash);   

                if (isPasswordCorrect)
                {
                    return Ok(await _service.Login(userLogin));
                }    

                else
                {
                    return BadRequest("Password is not correct");
                }
            }

            else
            {
                return BadRequest("User is not exists");
            }
        }
    }
}
