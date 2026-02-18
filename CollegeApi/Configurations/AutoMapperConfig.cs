using AutoMapper;
using CollegeApi.Data;
using CollegeApi.Models;

namespace CollegeApi.Configurations
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        { 
            CreateMap<Student,StudentDTO>().ReverseMap();
        }
    }
}
