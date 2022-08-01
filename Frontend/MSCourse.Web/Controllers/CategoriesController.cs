using Microsoft.AspNetCore.Mvc;
using MSCourse.Shared.Services.Interfaces;
using MSCourse.Web.Services.Interfaces;
using System.Threading.Tasks;

namespace MSCourse.Web.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICatalogService _catalogService;

        public CategoriesController(ICatalogService catalogService)
        {
            _catalogService = catalogService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _catalogService.GetAllCategoryAsync());
        }
    }
}
