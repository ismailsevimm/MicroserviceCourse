using MSCourse.Web.Models.CatalogModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MSCourse.Web.Services.Interfaces
{
    public interface ICatalogService
    {
        Task<List<CourseViewModel>> GetAllCourseAsync();

        Task<List<CategoryViewModel>> GetAllCategoryAsync();

        Task<List<CourseViewModel>> GetAllCourseByUserIdAsync(string userId);

        Task<List<CourseViewModel>> GetAllCourseByCategoryIdAsync(string categoryId);

        Task<CourseViewModel> GetByCourseIdAsync(string courseId);

        Task<bool> CreateCourseAsync(CourseCreateInput courseCreateInput);

        Task<bool> UpdateCourseAsync(CourseUpdateInput courseUpdateInput);

        Task<bool> DeleteCourseAsync(string courseId);

        Task<bool> CreateCategoryAsync(CategoryCreateInput categoryCreateInput);

        Task<bool> UpdateCategoryAsync(CategoryUpdateInput categoryUpdateInput);

        Task<bool> DeleteCategoryAsync(string courseId);
    }
}
