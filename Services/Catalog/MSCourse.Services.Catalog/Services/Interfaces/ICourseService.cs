using MSCourse.Services.Catalog.Dtos;
using MSCourse.Services.Catalog.Models;
using MSCourse.Shared.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MSCourse.Services.Catalog.Services.Interfaces
{
    public interface ICourseService
    {
        Task<Response<CourseDto>> CreateAsync(CourseCreateDto course);

        Task<Response<CourseDto>> UpdateAsync(CourseUpdateDto course);

        Task<Response<NoContent>> DeleteAsync(string id);

        Task<Response<CourseDto>> GetByIdAsync(string id);

        Task<Response<List<CourseDto>>> GetAllAsync();

        Task<Response<List<CourseDto>>> GetAllByUserIdAsync(string userId);

        Task<Response<List<CourseDto>>> GetAllByCategoryIdAsync(string categoryId);
    }
}
