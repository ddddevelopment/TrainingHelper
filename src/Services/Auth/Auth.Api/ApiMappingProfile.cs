using Auth.Api.Models;
using Auth.Domain.Models;
using AutoMapper;

namespace Auth.Api
{
    public class ApiMappingProfile : Profile
    {
        public ApiMappingProfile()
        {
            CreateMap<UserLoginRequest, UserLogin>().ReverseMap();
        }
    }
}
