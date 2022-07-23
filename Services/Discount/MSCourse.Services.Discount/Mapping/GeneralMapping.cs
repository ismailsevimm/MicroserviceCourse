using AutoMapper;
using MSCourse.Services.Discount.Dtos;

namespace MSCourse.Services.Discount.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            CreateMap<Models.Discount, DiscountDto>().ReverseMap();
            CreateMap<Models.Discount, DiscountCreateDto>().ReverseMap();
            CreateMap<Models.Discount, DiscountUpdateDto>().ReverseMap();
        }
    }
}
