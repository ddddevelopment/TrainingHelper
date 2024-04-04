using AutoMapper;
using Results.Api.Models;
using Results.Domain.Models;

namespace Results.Api
{
    public class ApiMappingProfile : Profile
    {
        public ApiMappingProfile() 
        {
            CreateMap<ResultPresentation, Result>().ReverseMap();
        }
    }
}
