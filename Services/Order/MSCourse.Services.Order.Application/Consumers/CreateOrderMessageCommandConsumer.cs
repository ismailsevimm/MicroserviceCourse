using MassTransit;
using MediatR;
using MSCourse.Services.Order.Infrastructure;
using MSCourse.Shared.MessageQueries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSCourse.Services.Order.Application.Consumers
{
    public class CreateOrderMessageCommandConsumer : IConsumer<CreateOrderMessageCommand>
    {
        private readonly OrderDbContext _orderDbContext;
        private readonly IMediator _mediator;

        public CreateOrderMessageCommandConsumer(OrderDbContext orderDbContext,IMediator mediator)
        {
            _orderDbContext = orderDbContext;
            _mediator = mediator;
        }

        public async Task Consume(ConsumeContext<CreateOrderMessageCommand> context)
        {
            //var response = await _mediator.Send(context.Message);

            try
            {
                var newAddress = new Domain.OrderAggregate.Address(context.Message.Address.Province, context.Message.Address.District, context.Message.Address.Street, context.Message.Address.ZipCode, context.Message.Address.Line);

                Domain.OrderAggregate.Order order = new Domain.OrderAggregate.Order(context.Message.BuyerId, newAddress);

                foreach (var item in context.Message.OrderItems)
                {
                    order.AddOrderItem(item.ProductId, item.ProductName, item.PictureUrl, item.Price);
                }

                await _orderDbContext.Orders.AddAsync(order);

                await _orderDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
