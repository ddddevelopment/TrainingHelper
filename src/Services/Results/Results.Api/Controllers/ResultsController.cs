using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
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
        private readonly IMapper _mapper;
        private readonly IValidator<ResultRequest> _validator;

        public ResultsController(IResultsService service, IMapper mapper, IValidator<ResultRequest> validator)
        {
            _service = service;
            _mapper = mapper;
            _validator = validator;
        }

        [HttpPost]
        public async Task<ActionResult> Create(ResultRequest resultPresentation)
        {
            ValidationResult validationResult = _validator.Validate(resultPresentation);

            if (validationResult.IsValid == false)
            {
                return BadRequest(validationResult.Errors);
            }

            Result result = new Result(Guid.NewGuid(), resultPresentation.Exercise, resultPresentation.WeightKg, resultPresentation.NumberOfRepetitions);
            await _service.Create(result);

            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<ResultRequest>> Get(Guid id)
        {
            Result result = await _service.Get(id);

            return Ok(result);
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _service.Delete(id);

            return Ok();
        }
    }
}
