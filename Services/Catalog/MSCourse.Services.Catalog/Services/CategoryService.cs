using AutoMapper;
using MongoDB.Driver;
using MSCourse.Services.Catalog.Dtos;
using MSCourse.Services.Catalog.Models;
using MSCourse.Services.Catalog.Services.Interfaces;
using MSCourse.Services.Catalog.Settings;
using MSCourse.Shared.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSCourse.Services.Catalog.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IMapper _mapper;

        public CategoryService(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var db = client.GetDatabase(databaseSettings.DatabaseName);

            _categoryCollection = db.GetCollection<Category>(databaseSettings.CategoryCollectionName);
            _mapper = mapper;
        }

        public async Task<Response<CategoryDto>> CreateAsync(CategoryCreateDto category)
        {
            var newCategory = _mapper.Map<Category>(category);

            await _categoryCollection.InsertOneAsync(newCategory);

            return Response<CategoryDto>.Success(_mapper.Map<CategoryDto>(newCategory), 200);
        }

        public async Task<Response<CategoryDto>> UpdateAsync(CategoryUpdateDto category)
        {
            var newCategory = _mapper.Map<Category>(category);

            var result = await _categoryCollection.FindOneAndReplaceAsync(ct => ct.Id == newCategory.Id, newCategory);

            if (result == null)
            {
                return Response<CategoryDto>.Fail("Category Not Found", 404);
            }

            return Response<CategoryDto>.Success(_mapper.Map<CategoryDto>(result), 204);
        }

        public async Task<Response<NoContent>> DeleteAsync(string id)
        {
            var result = await _categoryCollection.DeleteOneAsync(co => co.Id == id);

            if (result.DeletedCount <= 0)
            {
                return Response<NoContent>.Fail("Category Not Found", 404);
            }

            return Response<NoContent>.Success(204);
        }

        public async Task<Response<List<CategoryDto>>> GetAllAsync()
        {
            var categories = await _categoryCollection.Find(category => true).ToListAsync();

            categories = categories.OrderBy(cat => cat.Name).ToList();

            return Response<List<CategoryDto>>.Success(_mapper.Map<List<CategoryDto>>(categories), 200);
        }

        public async Task<Response<CategoryDto>> GetByIdAsync(string id)
        {
            var category = await _categoryCollection.Find(ct => ct.Id == id).FirstOrDefaultAsync();

            if (category == null)
            {
                return Response<CategoryDto>.Fail("Category not found", 404);
            }

            return Response<CategoryDto>.Success(_mapper.Map<CategoryDto>(category), 200);
        }
    }
}
