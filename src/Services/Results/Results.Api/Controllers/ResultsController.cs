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
    [Route("[controller]")]
    public class ResultsController : ControllerBase
    {
        private readonly IResultsService _service;
        private readonly IMapper _mapper;
        private readonly IValidator<ResultPresentation> _validator;

        public ResultsController(IResultsService service, IMapper mapper, IValidator<ResultPresentation> validator)
        {
            _service = service;
            _mapper = mapper;
            _validator = validator;
        }

        [HttpPost]
        public async Task<ActionResult> Create(ResultPresentation resultPresentation)
        {
            ValidationResult validationResult = _validator.Validate(resultPresentation);

            if (validationResult.IsValid == false)
            {
                return BadRequest(validationResult.Errors);
            }

            Result result = _mapper.Map<Result>(resultPresentation);
            await _service.Create(result);

            return Ok();
        }
    }
}
