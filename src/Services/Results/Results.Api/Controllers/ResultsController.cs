using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Results.Api.Models;
using Results.Domain.Abstractions.Services;
using Results.Domain.Models;

namespace Results.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResultsController : ControllerBase
    {
        private readonly IResultsService _service;
        private readonly IValidator<ResultRequest> _validator;

        public ResultsController(IResultsService service, IValidator<ResultRequest> validator)
        {
            _service = service;
            _validator = validator;
        }

        [HttpPost("create")]
        [Authorize]
        public async Task<ActionResult> Create(ResultRequest resultPresentation)
        {
            ValidationResult validationResult = _validator.Validate(resultPresentation);

            if (validationResult.IsValid == false)
            {
                return BadRequest(validationResult.Errors);
            }

            Result result = new Result(Guid.NewGuid(), resultPresentation.Exercise, resultPresentation.WeightKg, 
                resultPresentation.NumberOfRepetitions, resultPresentation.UserId);

            await _service.Create(result);

            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResultRequest>> Get(Guid id)
        {
            Result result = await _service.Get(id);

            return Ok(result);
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _service.Delete(id);

            return Ok();
        }
    }
}
