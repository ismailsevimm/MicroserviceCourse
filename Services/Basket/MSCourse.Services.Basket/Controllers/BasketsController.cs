using Microsoft.AspNetCore.Mvc;
using MSCourse.Services.Basket.Dtos;
using MSCourse.Services.Basket.Services.Interfaces;
using MSCourse.Shared.ControllerBases;
using MSCourse.Shared.Services.Interfaces;
using System.Threading.Tasks;

namespace MSCourse.Services.Basket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketsController : CustomControllerBase
    {
        private readonly IBasketService _basketService;
        private readonly ISharedIdentityService _sharedIdentityService;

        public BasketsController(IBasketService basketService, ISharedIdentityService sharedIdentityService)
        {
            _basketService = basketService;
            _sharedIdentityService = sharedIdentityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetBasket()
        {
            return CreateActionResultInstance(await _basketService.GetBasket(_sharedIdentityService.GetUserId));
        }

        [HttpPost]
        public async Task<IActionResult> SaveOrUpdate(BasketDto basket)
        {
            basket.UserId = _sharedIdentityService.GetUserId;
            return CreateActionResultInstance(await _basketService.SaveOrUpdate(basket));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete()
        {
            return CreateActionResultInstance(await _basketService.Delete(_sharedIdentityService.GetUserId));
        }
    }
}
