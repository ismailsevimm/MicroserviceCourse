using MSCourse.Shared.Dtos;
using MSCourse.Web.Models.BasketModels;
using MSCourse.Web.Services.Interfaces;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MSCourse.Web.Services
{
    public class BasketService : IBasketService
    {
        private readonly HttpClient _httpClient;
        private readonly IDiscountService _discountService;

        public BasketService(HttpClient httpClient, IDiscountService discountService)
        {
            _httpClient = httpClient;
            _discountService = discountService;
        }

        public async Task AddBasketItem(BasketItemViewModel basketItemViewModel)
        {
            var basket = await Get();

            if (basket != null)
            {
                if (!basket.BasketItems.Any(x => x.CourseId == basketItemViewModel.CourseId))
                {
                    basket.BasketItems.Add(basketItemViewModel);
                }
            }
            else
            {
                basket = new BasketViewModel();

                basket.BasketItems.Add(basketItemViewModel);
            }

            await SaveOrUpdate(basket);
        }

        public async Task<bool> ApplyDiscount(string discountCode)
        {
            var cancelResult = await CancelApplyDiscount();

            if (!cancelResult)
                return false;

            var basket = await Get();

            if (basket == null)
                return false;

            var hasDiscount = await _discountService.Get(discountCode);

            if (hasDiscount == null || hasDiscount.ActivationEndTime <= DateTime.Now)
                return false;

            basket.ApplyDiscount(hasDiscount.Code, hasDiscount.Rate);

            return await SaveOrUpdate(basket);

        }

        public async Task<bool> CancelApplyDiscount()
        {
            var basket = await Get();

            if (basket == null)
            {
                return false;
            }

            basket.CancelDiscount();

            return await SaveOrUpdate(basket);
        }

        public async Task<bool> Delete()
        {
            var response = await _httpClient.DeleteAsync("baskets");

            return response.IsSuccessStatusCode;
        }

        public async Task<BasketViewModel> Get()
        {
            var response = await _httpClient.GetAsync("baskets");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var basketViewModel = await response.Content.ReadFromJsonAsync<Response<BasketViewModel>>();

            return basketViewModel.Data;
        }

        public async Task<bool> RemoveBasketItem(string courseId)
        {
            var basket = await Get();

            if (basket == null)
            {
                return false;
            }

            var basketItem = basket.BasketItems.FirstOrDefault(x => x.CourseId == courseId);

            if (basketItem == null)
            {
                return false;
            }

            var deleteResult = basket.BasketItems.Remove(basketItem);

            if (!deleteResult)
            {
                return false;
            }

            if (!basket.BasketItems.Any())
            {
                basket.CancelDiscount();
            }

            return await SaveOrUpdate(basket);
        }

        public async Task<bool> SaveOrUpdate(BasketViewModel basketViewModel)
        {
            var response = await _httpClient.PostAsJsonAsync<BasketViewModel>("baskets", basketViewModel);

            return response.IsSuccessStatusCode;
        }
    }
}
