using Microsoft.AspNetCore.Mvc;
using MSCourse.Services.Discount.Dtos;
using MSCourse.Services.Discount.Services.Interfaces;
using MSCourse.Shared.ControllerBases;
using MSCourse.Shared.Services.Interfaces;
using System.Threading.Tasks;

namespace MSCourse.Services.Discount.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DiscountsController : CustomControllerBase
    {
        private readonly IDiscountService _discountService;
        private readonly ISharedIdentityService _sharedIdentityService;

        public DiscountsController(IDiscountService discountService, ISharedIdentityService sharedIdentityService)
        {
            _discountService = discountService;
            _sharedIdentityService = sharedIdentityService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(DiscountCreateDto discount)
        {
            return CreateActionResultInstance(await _discountService.Create(discount));
        }

        [HttpPost]
        public async Task<IActionResult> Update(DiscountUpdateDto discount)
        {
            return CreateActionResultInstance(await _discountService.Update(discount));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return CreateActionResultInstance(await _discountService.Delete(id));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteByUserId()
        {
            return CreateActionResultInstance(await _discountService.DeleteByUserId(_sharedIdentityService.GetUserId));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return CreateActionResultInstance(await _discountService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return CreateActionResultInstance(await _discountService.GetById(id));
        }

        [HttpGet]
        public async Task<IActionResult> GetByUserId()
        {
            return CreateActionResultInstance(await _discountService.GetByUserId(_sharedIdentityService.GetUserId));
        }

        [HttpPost]
        public async Task<IActionResult> GetByCodeAndUserId(DiscountGetByCodeAndUserIdDto dto)
        {
            return CreateActionResultInstance(await _discountService.GetByCodeAndUserId(dto.Code, dto.UserId != null ? dto.UserId : _sharedIdentityService.GetUserId));
        }
    }
}
