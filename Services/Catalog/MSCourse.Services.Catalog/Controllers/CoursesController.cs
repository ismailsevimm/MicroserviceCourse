using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MSCourse.Services.Catalog.Dtos;
using MSCourse.Services.Catalog.Services.Interfaces;
using MSCourse.Shared.ControllerBases;
using System.Threading.Tasks;

namespace MSCourse.Services.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : CustomControllerBase
    {
        private readonly ICourseService _courseService;

        public CoursesController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        //courses/

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _courseService.GetAllAsync();

            return CreateActionResultInstance(response);
        }

        //courses/4

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var response = await _courseService.GetByIdAsync(id);

            return CreateActionResultInstance(response);
        }

        //api/courses/GetAllByUserId/a5s12-121f3-12svd-2as6g
        [HttpGet]
        [Route("/api/[controller]/GetAllByUserId/{userId}")]
        public async Task<IActionResult> GetAllByUserId(string userId)
        {
            var response = await _courseService.GetAllByUserIdAsync(userId);

            return CreateActionResultInstance(response);
        }
        
        //api/courses/GetAllByCategoryId/as2576dgsd124sadfg
        [HttpGet]
        [Route("/api/[controller]/GetAllByCategoryId/{categoryId}")]
        public async Task<IActionResult> GetAllByCategoryId(string categoryId)
        {
            var response = await _courseService.GetAllByCategoryIdAsync(categoryId);

            return CreateActionResultInstance(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CourseCreateDto course)
        {
            var response = await _courseService.CreateAsync(course);

            return CreateActionResultInstance(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update(CourseUpdateDto course)
        {
            var response = await _courseService.UpdateAsync(course);

            return CreateActionResultInstance(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _courseService.DeleteAsync(id);

            return CreateActionResultInstance(response);
        }

    }
}
