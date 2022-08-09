using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MSCourse.Web.Models.BasketModels;
using MSCourse.Web.Models.DiscountModels;
using MSCourse.Web.Services.Interfaces;
using System.Threading.Tasks;

namespace MSCourse.Web.Controllers
{
    [Authorize]
    public class BasketsController : Controller
    {
        private readonly ICatalogService _catalogService;
        private readonly IBasketService _basketService;
        private readonly IDiscountService _discountService;

        public BasketsController(ICatalogService catalogService, IBasketService basketService, IDiscountService discountService)
        {
            _catalogService = catalogService;
            _basketService = basketService;
            _discountService = discountService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _basketService.Get());
        }

        public async Task<IActionResult> AddBasketItem(string courseId)
        {
            var course = await _catalogService.GetByCourseIdAsync(courseId);

            var basketItem = new BasketItemViewModel() 
            {
                CourseId = course.Id,
                CourseName = course.Name,
                Price = course.Price
            };

            await _basketService.AddBasketItem(basketItem);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> RemoveBasketItem(string courseId)
        {
            var result = await _basketService.RemoveBasketItem(courseId);

            if (!result)
                return View();

            
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ApplyDiscount(DiscountApplyInput discountInput)
        {
            var discountStatus = await _basketService.ApplyDiscount(discountInput.Code);

            TempData["discountStatus"] = discountStatus;

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> CancelApplyDiscount()
        {
            var discountStatus = await _basketService.CancelApplyDiscount();

            TempData["discountStatus"] = discountStatus;

            return RedirectToAction(nameof(Index));
        }

    }
}
