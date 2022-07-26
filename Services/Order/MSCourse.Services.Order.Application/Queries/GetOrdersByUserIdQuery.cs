using MediatR;
using MSCourse.Services.Order.Application.Dtos;
using MSCourse.Shared.Dtos;
using System.Collections.Generic;

namespace MSCourse.Services.Order.Application.Queries
{
    public class GetOrdersByUserIdQuery : IRequest<Response<List<OrderDto>>>
    {
        public string UserId { get; set; }
    }
}
