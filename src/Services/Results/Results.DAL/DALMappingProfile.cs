using AutoMapper;
using Results.DAL.Entities;
using Results.Domain.Models;

namespace Results.DAL
{
    public class DALMappingProfile : Profile
    {
        public DALMappingProfile() 
        {
            CreateMap<Result, ResultEntity>().ReverseMap();
        }
    }
}
