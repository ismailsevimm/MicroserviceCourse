using MSCourse.Services.Catalog.Dtos;
using MSCourse.Services.Catalog.Models;
using MSCourse.Shared.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MSCourse.Services.Catalog.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<Response<CategoryDto>> CreateAsync(CategoryCreateDto category);

        Task<Response<CategoryDto>> UpdateAsync(CategoryUpdateDto category);

        Task<Response<NoContent>> DeleteAsync(string id);

        Task<Response<CategoryDto>> GetByIdAsync(string id);

        Task<Response<List<CategoryDto>>> GetAllAsync();
    }
}
