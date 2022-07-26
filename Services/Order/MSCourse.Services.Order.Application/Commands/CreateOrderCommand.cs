using MediatR;
using MSCourse.Services.Order.Application.Dtos;
using MSCourse.Shared.Dtos;
using System.Collections.Generic;

namespace MSCourse.Services.Order.Application.Commands
{
    public class CreateOrderCommand:IRequest<Response<OrderCreatedDto>>
    {
        public string BuyerId { get; set; }

        public AddressDto Address { get; set; }

        public List<OrderItemDto> OrderItems { get; set; }
    }
}
