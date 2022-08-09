
using MSCourse.Shared.Dtos;
using MSCourse.Shared.Services.Interfaces;
using MSCourse.Web.Models.DiscountModels;
using MSCourse.Web.Services.Interfaces;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MSCourse.Web.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly HttpClient _httpClient;
        private readonly ISharedIdentityService _sharedIdentityService;

        public DiscountService(HttpClient httpClient, ISharedIdentityService sharedIdentityService)
        {
            _httpClient = httpClient;
            _sharedIdentityService = sharedIdentityService;
        }

        public async Task<DiscountViewModel> Get(string discountCode)
        {
            var response = await _httpClient.PostAsJsonAsync<DiscountGetInput>($"discounts/GetByCodeAndUserId/", new DiscountGetInput { Code = discountCode, UserId = _sharedIdentityService.GetUserId });

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var discountResult = await response.Content.ReadFromJsonAsync<Response<DiscountViewModel>>();

            return discountResult.Data;
        }
    }
}
