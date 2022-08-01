using MSCourse.Shared.Dtos;
using MSCourse.Web.Models.CatalogModels;
using MSCourse.Web.Services.Interfaces;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MSCourse.Web.Services
{
    public class CatalogService : ICatalogService
    {
        private HttpClient _httpClient;

        public CatalogService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        #region Course
        public async Task<bool> CreateCourseAsync(CourseCreateInput courseCreateInput)
        {
            var clientResponse = await _httpClient.PostAsJsonAsync<CourseCreateInput>($"courses", courseCreateInput);

            return clientResponse.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateCourseAsync(CourseUpdateInput courseUpdateInput)
        {
            var clientResponse = await _httpClient.PutAsJsonAsync<CourseUpdateInput>($"courses", courseUpdateInput);

            return clientResponse.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteCourseAsync(string courseId)
        {
            var clientResponse = await _httpClient.DeleteAsync($"courses/{courseId}");

            return clientResponse.IsSuccessStatusCode;
        }

        public async Task<List<CourseViewModel>> GetAllCourseAsync()
        {
            var clientResponse = await _httpClient.GetAsync("courses");

            if (!clientResponse.IsSuccessStatusCode)
            {
                return null;
            }

            var response = await clientResponse.Content.ReadFromJsonAsync<Response<List<CourseViewModel>>>();

            return response.Data;
        }

        public async Task<List<CourseViewModel>> GetAllCourseByCategoryIdAsync(string categoryId)
        {
            var clientResponse = await _httpClient.GetAsync($"courses/GetAllByCategoryId/{categoryId}");

            if (!clientResponse.IsSuccessStatusCode)
            {
                return null;
            }

            var response = await clientResponse.Content.ReadFromJsonAsync<Response<List<CourseViewModel>>>();

            return response.Data;
        }

        public async Task<List<CourseViewModel>> GetAllCourseByUserIdAsync(string userId)
        {
            var clientResponse = await _httpClient.GetAsync($"courses/GetAllByUserId/{userId}");

            if (!clientResponse.IsSuccessStatusCode)
            {
                return null;
            }

            var response = await clientResponse.Content.ReadFromJsonAsync<Response<List<CourseViewModel>>>();

            return response.Data;
        }

        public async Task<CourseViewModel> GetByCourseIdAsync(string courseId)
        {
            var clientResponse = await _httpClient.GetAsync($"courses/{courseId}");

            if (!clientResponse.IsSuccessStatusCode)
            {
                return null;
            }

            var response = await clientResponse.Content.ReadFromJsonAsync<Response<CourseViewModel>>();

            return response.Data;
        }

        #endregion

        #region Category
        public async Task<bool> CreateCategoryAsync(CategoryCreateInput categoryCreateInput)
        {
            var clientResponse = await _httpClient.PutAsJsonAsync<CategoryCreateInput>("categories", categoryCreateInput);

            return clientResponse.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateCategoryAsync(CategoryUpdateInput categoryUpdateInput)
        {
            var clientResponse = await _httpClient.PutAsJsonAsync<CategoryUpdateInput>("categories",categoryUpdateInput);

            return clientResponse.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteCategoryAsync(string courseId)
        {
            var clientResponse = await _httpClient.DeleteAsync($"categories/{courseId}");

            return clientResponse.IsSuccessStatusCode;
        }

        public async Task<List<CategoryViewModel>> GetAllCategoryAsync()
        {
            var clientResponse = await _httpClient.GetAsync("categories/");

            if (!clientResponse.IsSuccessStatusCode)
            {
                return null;
            }

            var response = await clientResponse.Content.ReadFromJsonAsync<Response<List<CategoryViewModel>>>();

            return response.Data;
        }
        #endregion
    }
}
