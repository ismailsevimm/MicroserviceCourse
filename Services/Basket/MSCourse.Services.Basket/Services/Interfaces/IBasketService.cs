using MSCourse.Services.Basket.Dtos;
using MSCourse.Shared.Dtos;
using System.Threading.Tasks;

namespace MSCourse.Services.Basket.Services.Interfaces
{
    public interface IBasketService
    {
        Task<Response<BasketDto>> GetBasket(string userId);
        Task<Response<bool>> SaveOrUpdate(BasketDto basket);
        Task<Response<bool>> Delete(string userId);

    }
}
