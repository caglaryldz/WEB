using AutoMapper;
using WebApplication2.Entities;
using WebApplication2.Models;

namespace WebApplication2
{
    public class AuttoMapperConfig : Profile
    {
        public AuttoMapperConfig() 
        {
            CreateMap<User, UserModel>().ReverseMap();

        }
           
    }
}
