using MSCourse.Web.Models.BasketModels;
using MSCourse.Web.Services.Interfaces;
using System.Net.Http;
using System.Threading.Tasks;

namespace MSCourse.Web.Services
{
    public class BasketService : IBasketService
    {
        private readonly HttpClient _httpClient;

        public BasketService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task<bool> AddBasketItem(BasketItemViewModel basketItemViewModel)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> ApplyDiscount(string discountCode)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> CancelApplyDiscount()
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> Delete()
        {
            throw new System.NotImplementedException();
        }

        public Task<BasketViewModel> Get()
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> RemoveBasketItem(string productId)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> SaveOrUpdate(BasketViewModel basketViewModel)
        {
            throw new System.NotImplementedException();
        }
    }
}
