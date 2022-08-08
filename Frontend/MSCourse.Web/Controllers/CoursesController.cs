using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MSCourse.Shared.Services.Interfaces;
using MSCourse.Web.Models.CatalogModels;
using MSCourse.Web.Services.Interfaces;
using System.Threading.Tasks;

namespace MSCourse.Web.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ICatalogService _catalogService;
        private readonly ISharedIdentityService _sharedIdentityService;

        public CoursesController(ICatalogService catalogService, ISharedIdentityService sharedIdentityService)
        {
            _catalogService = catalogService;
            _sharedIdentityService = sharedIdentityService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _catalogService.GetAllCourseByUserIdAsync(_sharedIdentityService.GetUserId));
        }

        public async Task<IActionResult> Create()
        {
            var cats = await _catalogService.GetAllCategoryAsync();

            ViewBag.categoryList = new SelectList(cats, "Id", "Name");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CourseCreateInput courseCreateInput)
        {
            var cats = await _catalogService.GetAllCategoryAsync();

            ViewBag.categoryList = new SelectList(cats, "Id", "Name");

            if (!ModelState.IsValid)
            {
                return View();
            }

            courseCreateInput.UserId = _sharedIdentityService.GetUserId;

            await _catalogService.CreateCourseAsync(courseCreateInput);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (id is null)
            {
                return View();
            }

            await _catalogService.DeleteCourseAsync(id);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(string id)
        {
            var course = await _catalogService.GetByCourseIdAsync(id);

            var cats = await _catalogService.GetAllCategoryAsync();

            if (course == null)
            {
                RedirectToAction(nameof(Index));
            }

            ViewBag.categoryList = new SelectList(cats, "Id", "Name", course.Id);

            CourseUpdateInput courseUpdateInput = new()
            {
                Id = course.Id,
                Name = course.Name,
                Price = course.Price,
                Feature = course.Feature,
                Description = course.Description,
                Picture = course.Picture,
                PictureUrl = course.PictureUrl,
                UserId = course.UserId,
                CategoryId = course.CategoryId
            };

            return View(courseUpdateInput);
        }

        [HttpPost]
        public async Task<IActionResult> Update(CourseUpdateInput courseUpdateInput)
        {
            var cats = await _catalogService.GetAllCategoryAsync();

            ViewBag.categoryList = new SelectList(cats, "Id", "Name", courseUpdateInput.Id);

            if (!ModelState.IsValid)
            {
                return View();
            }

            await _catalogService.UpdateCourseAsync(courseUpdateInput);

            return RedirectToAction(nameof(Index));
        }

    }
}
