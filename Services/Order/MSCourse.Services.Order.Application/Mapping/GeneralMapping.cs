using AutoMapper;
using MSCourse.Services.Order.Application.Dtos;

namespace MSCourse.Services.Order.Application.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            CreateMap<Domain.OrderAggregate.Order, OrderDto>().ReverseMap();
            CreateMap<Domain.OrderAggregate.OrderItem, OrderItemDto>().ReverseMap();
            CreateMap<Domain.OrderAggregate.Address, AddressDto>().ReverseMap();
        }
    }
}
