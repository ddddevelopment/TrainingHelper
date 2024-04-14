using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Users.Api.Models;
using Users.Domain.Abstractions.Services;
using Users.Domain.Models;

namespace Users.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _service;
        private readonly IMapper _mapper;

        public UsersController(IUsersService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult> Create(UserRequest userRequest)
        {
            User user = new User(Guid.NewGuid(), userRequest.Name, userRequest.Email);

            await _service.Create(user);

            return Ok(user);
        }

        [HttpGet]
        public async Task<ActionResult> Get(Guid id)
        {
            User user = await _service.Get(id);

            return Ok(user);
        }
    }
}
