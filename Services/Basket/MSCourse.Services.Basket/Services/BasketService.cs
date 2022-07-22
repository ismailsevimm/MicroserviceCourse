using MSCourse.Services.Basket.Dtos;
using MSCourse.Services.Basket.Services.Interfaces;
using MSCourse.Shared.Dtos;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace MSCourse.Services.Basket.Services
{
    public class BasketService : IBasketService
    {
        private readonly RedisService _redisService;

        public BasketService(RedisService redisService)
        {
            _redisService = redisService;
        }

        public async Task<Response<bool>> SaveOrUpdate(BasketDto basket)
        {
            var status = await _redisService.GetDatabase().StringSetAsync(basket.UserId, JsonSerializer.Serialize(basket));

            return status ? Response<bool>.Success(204) : Response<bool>.Fail("Basket Could Not Save Or Update", 500);
        }

        public async Task<Response<bool>> Delete(string userId)
        {
            var status = await _redisService.GetDatabase().KeyDeleteAsync(userId);

            return status ? Response<bool>.Success(204) : Response<bool>.Fail("Basket Not Found", 404); ;
        }

        public async Task<Response<BasketDto>> GetBasket(string userId)
        {
            var existsBasket = await _redisService.GetDatabase().StringGetAsync(userId);

            if (String.IsNullOrEmpty(existsBasket))
                return Response<BasketDto>.Fail("Basket Not Found", 404);

            return Response<BasketDto>.Success(JsonSerializer.Deserialize<BasketDto>(existsBasket), 200);
        }


    }
}
