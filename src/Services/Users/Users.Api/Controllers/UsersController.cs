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

        public UsersController(IUsersService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult> Create(UserPresentation userPresentation)
        {
            User user = new User(userPresentation.Name, userPresentation.Email);

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
