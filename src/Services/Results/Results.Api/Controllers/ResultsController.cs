using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Results.Api.Models;
using Results.Domain.Abstractions.Services;
using Results.Domain.Models;

namespace Results.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ResultsController : ControllerBase
    {
        private readonly IResultsService _service;
        private readonly IMapper _mapper;

        public ResultsController(IResultsService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult> Create(ResultPresentation resultPresentation)
        {
            Result result = _mapper.Map<Result>(resultPresentation);
            await _service.Create(result);

            return Ok(result);
        }
    }
}
