using MassTransit;
using MSCourse.Services.Basket.Dtos;
using MSCourse.Services.Basket.Services;
using MSCourse.Shared.MessageQueries;
using MSCourse.Shared.Services.Interfaces;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace MSCourse.Services.Basket.Consumers
{
    public class CourseNameChangedEventConsumer : IConsumer<CourseNameChangedEvent>
    {
        private readonly RedisService _redisService;

        public CourseNameChangedEventConsumer(RedisService redisService)
        {
            _redisService = redisService;
        }

        public async Task Consume(ConsumeContext<CourseNameChangedEvent> context)
        {
            var redisKeys = _redisService.GetAllKeys();

            foreach (var key in redisKeys)
            {
                var redisBasket = await _redisService.GetDatabase().StringGetAsync(key);

                var basket = JsonSerializer.Deserialize<BasketDto>(redisBasket);

                var basketItems = basket.BasketItems.Where(x => x.CourseId == context.Message.CourseId).ToList();

                basketItems.ForEach(item => {

                    item.CourseName = context.Message.UpdatedCourseName;

                });

                basket.BasketItems = basketItems;

                await _redisService.GetDatabase().StringSetAsync(basket.UserId, JsonSerializer.Serialize(basket));
            }
        }
    }
}
