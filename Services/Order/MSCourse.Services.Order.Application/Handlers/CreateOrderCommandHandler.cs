using MediatR;
using MSCourse.Services.Order.Application.Commands;
using MSCourse.Services.Order.Application.Dtos;
using MSCourse.Services.Order.Domain.OrderAggregate;
using MSCourse.Services.Order.Infrastructure;
using MSCourse.Shared.Dtos;
using System.Threading;
using System.Threading.Tasks;

namespace MSCourse.Services.Order.Application.Handlers
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Response<OrderCreatedDto>>
    {
        private readonly OrderDbContext _context;

        public CreateOrderCommandHandler(OrderDbContext context)
        {
            _context = context;
        }

        public async Task<Response<OrderCreatedDto>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var newAddress = new Address(request.Address.Province, request.Address.District, request.Address.Street, request.Address.ZipCode, request.Address.Line);

            Domain.OrderAggregate.Order newOrder = new Domain.OrderAggregate.Order(request.BuyerId, newAddress);

            request.OrderItems.ForEach(
                x =>
                {
                    newOrder.AddOrderItem(x.ProductId, x.ProductName, x.PictureUrl, x.Price);
                });

            await _context.Orders.AddAsync(newOrder);
            await _context.SaveChangesAsync();

            return Response<OrderCreatedDto>.Success(new() { OrderId = newOrder.Id }, 200);
        }
    }
}
