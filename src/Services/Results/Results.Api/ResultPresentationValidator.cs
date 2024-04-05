using FluentValidation;
using Results.Api.Models;

namespace Results.Api
{
    public class ResultPresentationValidator : AbstractValidator<ResultPresentation>
    {
        public ResultPresentationValidator()
        {
            RuleFor(result => result.Exercise).NotEmpty();
            RuleFor(result => result.WeightKg).GreaterThan(0).NotEmpty();
            RuleFor(result => result.NumberOfRepetitions).GreaterThan(0).NotEmpty();
        }
    }
}
