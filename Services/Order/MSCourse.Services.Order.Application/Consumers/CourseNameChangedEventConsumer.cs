using MassTransit;
using Microsoft.EntityFrameworkCore;
using MSCourse.Services.Order.Infrastructure;
using MSCourse.Shared.MessageQueries;
using System.Linq;
using System.Threading.Tasks;

namespace MSCourse.Services.Order.Application.Consumers
{
    public class CourseNameChangedEventConsumer : IConsumer<CourseNameChangedEvent>
    {
        private readonly OrderDbContext _orderDbContext;

        public CourseNameChangedEventConsumer(OrderDbContext orderDbContext)
        {
            _orderDbContext = orderDbContext;
        }

        public async Task Consume(ConsumeContext<CourseNameChangedEvent> context)
        {
            var orderItems = await _orderDbContext.OrderItems.Where(x => x.ProductId == context.Message.CourseId).ToListAsync();

            orderItems.ForEach(item => {
                item.UpdateProductName(context.Message.UpdatedCourseName);
            });

            await _orderDbContext.SaveChangesAsync();
        }
    }
}
