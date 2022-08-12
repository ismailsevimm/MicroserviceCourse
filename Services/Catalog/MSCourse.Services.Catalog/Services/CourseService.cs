using AutoMapper;
using Masstrans = MassTransit;
using MongoDB.Driver;
using MSCourse.Services.Catalog.Dtos;
using MSCourse.Services.Catalog.Models;
using MSCourse.Services.Catalog.Services.Interfaces;
using MSCourse.Services.Catalog.Settings;
using MSCourse.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MSCourse.Shared.MessageQueries;

namespace MSCourse.Services.Catalog.Services
{
    public class CourseService : ICourseService
    {
        private readonly IMongoCollection<Course> _courseCollection;
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IMapper _mapper;
        private readonly Masstrans.IPublishEndpoint _publishEndpoint;

        public CourseService(IMapper mapper, IDatabaseSettings databaseSettings, Masstrans.IPublishEndpoint publishEndpoint)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var db = client.GetDatabase(databaseSettings.DatabaseName);

            _courseCollection = db.GetCollection<Course>(databaseSettings.CourseCollectionName);
            _categoryCollection = db.GetCollection<Category>(databaseSettings.CategoryCollectionName);
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<Response<CourseDto>> CreateAsync(CourseCreateDto course)
        {
            var newCourse = _mapper.Map<Course>(course);

            newCourse.CreatedTime = DateTime.Now;

            await _courseCollection.InsertOneAsync(newCourse);

            newCourse.Category = await _categoryCollection.Find(cat => cat.Id == newCourse.CategoryId).FirstAsync();

            return Response<CourseDto>.Success(_mapper.Map<CourseDto>(newCourse), 200);
        }

        public async Task<Response<CourseDto>> UpdateAsync(CourseUpdateDto course)
        {
            var updatingCourse = await _courseCollection.Find(co => co.Id == course.Id).FirstOrDefaultAsync();

            if (updatingCourse == null)
            {
                return Response<CourseDto>.Fail("Course Not Found", 404);
            }

            var newCourse = _mapper.Map<Course>(course);

            newCourse.CreatedTime = updatingCourse.CreatedTime;

            var result = await _courseCollection.FindOneAndReplaceAsync(co => co.Id == newCourse.Id, newCourse);

            if (result == null)
            {
                return Response<CourseDto>.Fail("Course Not Found", 404);
            }

            await _publishEndpoint.Publish<CourseNameChangedEvent>(new CourseNameChangedEvent { CourseId = newCourse.Id, UpdatedCourseName = newCourse.Name });

            return Response<CourseDto>.Success(_mapper.Map<CourseDto>(result), 204);
        }

        public async Task<Response<NoContent>> DeleteAsync(string id)
        {
            var result = await _courseCollection.DeleteOneAsync(co => co.Id == id);

            if (result.DeletedCount <= 0)
            {
                return Response<NoContent>.Fail("Course Not Found", 404);
            }

            return Response<NoContent>.Success(204);
        }

        public async Task<Response<CourseDto>> GetByIdAsync(string id)
        {
            var course = await _courseCollection.Find(co => co.Id == id).FirstOrDefaultAsync();

            if (course == null)
            {
                return Response<CourseDto>.Fail("Course Not Found", 404);
            }

            course.Category = await _categoryCollection.Find(ct => ct.Id == course.CategoryId).FirstAsync();

            return Response<CourseDto>.Success(_mapper.Map<CourseDto>(course), 200);
        }

        public async Task<Response<List<CourseDto>>> GetAllAsync()
        {
            var courses = await _courseCollection.Find(co => true).ToListAsync();

            if (courses.Any())
            {
                foreach (var course in courses)
                {
                    course.Category = await _categoryCollection.Find(ct => ct.Id == course.CategoryId).FirstAsync();
                }

                courses = courses.OrderBy(co => co.Name).ToList();
            }
            else
            {
                courses = new List<Course>();
            }

            return Response<List<CourseDto>>.Success(_mapper.Map<List<CourseDto>>(courses), 200);
        }

        public async Task<Response<List<CourseDto>>> GetAllByUserIdAsync(string userId)
        {
            var courses = await _courseCollection.Find(co => co.UserId == userId).ToListAsync();

            if (courses.Any())
            {
                foreach (var course in courses)
                {
                    course.Category = await _categoryCollection.Find(ct => ct.Id == course.CategoryId).FirstAsync();
                }

                courses = courses.OrderByDescending(co => co.CreatedTime).ToList();
            }
            else
            {
                courses = new List<Course>();
            }

            return Response<List<CourseDto>>.Success(_mapper.Map<List<CourseDto>>(courses), 200);
        }

        public async Task<Response<List<CourseDto>>> GetAllByCategoryIdAsync(string categoryId)
        {
            var courses = await _courseCollection.Find(co => co.CategoryId == categoryId).ToListAsync();

            if (courses.Any())
            {
                foreach (var course in courses)
                {
                    course.Category = await _categoryCollection.Find(ct => ct.Id == course.CategoryId).FirstAsync();
                }

                courses = courses.OrderByDescending(co => co.CreatedTime).ToList();
            }
            else
            {
                courses = new List<Course>();
            }

            return Response<List<CourseDto>>.Success(_mapper.Map<List<CourseDto>>(courses), 200);
        }
    }
}
