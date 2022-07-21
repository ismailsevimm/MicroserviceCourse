using AutoMapper;
using MSCourse.IdentityServer.Dtos;
using MSCourse.IdentityServer.Models;

namespace MSCourse.IdentityServer.Mapping
{
    public class GeneralMapping:Profile
    {
        public GeneralMapping()
        {
            CreateMap<ApplicationUser, UserDto>().ReverseMap();
            CreateMap<ApplicationUser, SignupDto>().ReverseMap();
        }
    }
}
