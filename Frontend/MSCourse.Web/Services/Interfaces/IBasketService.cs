using MSCourse.Web.Models.BasketModels;
using System.Threading.Tasks;

namespace MSCourse.Web.Services.Interfaces
{
    public interface IBasketService
    {
        Task<bool> SaveOrUpdate(BasketViewModel basketViewModel);

        Task<BasketViewModel> Get();

        Task<bool> Delete();

        Task<bool> AddBasketItem(BasketItemViewModel basketItemViewModel);

        Task<bool> RemoveBasketItem(string productId);

        Task<bool> ApplyDiscount(string discountCode);

        Task<bool> CancelApplyDiscount();

    }
}
