using AutoMapper;
using Users.DAL.Models;
using Users.Domain.Models;

namespace Users.DAL
{
    public class DALMappingProfile : Profile
    {
        public DALMappingProfile()
        {
            CreateMap<User, UserEntity>().ReverseMap();
        }
    }
}
