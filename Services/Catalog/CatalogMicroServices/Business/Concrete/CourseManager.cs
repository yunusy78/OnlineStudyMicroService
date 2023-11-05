using AutoMapper;
using CatalogMicroServices.Business.Abstract;
using CatalogMicroServices.Dtos.Course;
using CatalogMicroServices.Model;
using CatalogMicroServices.Settings;
using MassTransit;
using MongoDB.Driver;
using OnlineStudyShared;
using OnlineStudyShared.Message.Event;

namespace CatalogMicroServices.Business.Concrete;

public class CourseManager : ICourseService
{
    private readonly IMongoCollection<Course> _courseCollection;
    private readonly IMongoCollection<Category> _categoryCollection;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly IMapper _mapper;
    
    public CourseManager(IDatabaseSettings databaseSettings, IMapper mapper, IPublishEndpoint publishEndpoint)
    {
        var client = new MongoClient(databaseSettings.ConnectionString);
        var database = client.GetDatabase(databaseSettings.DatabaseName);
        _courseCollection = database.GetCollection<Course>(databaseSettings.CourseCollectionName);
        _categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);
        _mapper = mapper;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<ResponseDto<List<CourseDto>>> GetAllAsync()
    {
        var courses = await _courseCollection.Find(category => true).ToListAsync();
        if (courses.Any())
        {
            foreach (var item in courses)
            {
                item.Category = await _categoryCollection.Find<Category>(category => category.CategoryId == item.CategoryId).FirstOrDefaultAsync();
            }
        }
        else 
        {
            courses = new List<Course>();
        }
        
        return ResponseDto<List<CourseDto>>.Success(_mapper.Map<List<CourseDto>>(courses), 200);
        
    }

    public Task<ResponseDto<CreateCourseDto>> CreateAsync(Course t)
    {
        throw new NotImplementedException();
    }

    public async Task<ResponseDto<CourseDto>> CreateAsync(CreateCourseDto t)
    {
       var newCourse =_mapper.Map<Course>(t);
         newCourse.CourseCreatedDate = DateTime.Now;
            await _courseCollection.InsertOneAsync(newCourse);
            return ResponseDto<CourseDto>.Success(_mapper.Map<CourseDto>(newCourse), 200);
        
    }

    
    public async Task<ResponseDto<CourseDto>> GetByIdAsync(string id)
    {
        var course = await _courseCollection.Find<Course>(course => course.CourseId == id).FirstOrDefaultAsync();
        if (course == null)
        {
            return ResponseDto<CourseDto>.Fail("Course not found", 404);
        }
        course.Category = await _categoryCollection.Find<Category>(category => category.CategoryId == course.CategoryId).FirstOrDefaultAsync();
        return ResponseDto<CourseDto>.Success(_mapper.Map<CourseDto>(course), 200);
    }

    public async Task<ResponseDto<List<CourseDto>>> GetAllByUserAsync(string userId)
    {
        var courses = await _courseCollection.Find(course => course.UserId == userId).ToListAsync();
        if (courses.Any())
        {
            foreach (var item in courses)
            {
                item.Category = await _categoryCollection.Find<Category>(category => category.CategoryId == item.CategoryId).FirstOrDefaultAsync();
            }
        }
        else 
        {
            courses = new List<Course>();
        }
        
        return ResponseDto<List<CourseDto>>.Success(_mapper.Map<List<CourseDto>>(courses), 200);
        
    }

    public async Task<ResponseDto<NoContent>> UpdateAsync(CourseUpdateDto t)
    {
        var updateCourse = _mapper.Map<Course>(t);
        var result = await _courseCollection.FindOneAndReplaceAsync(course => course.CourseId == t.CourseId, updateCourse);
        if (result == null)
        {
            return ResponseDto<NoContent>.Fail("Course not found", 404);
        }
        await _publishEndpoint.Publish<CourseNameChangeEvent>(new
        {
            CourseId = updateCourse.CourseId,
            NewCourseName = updateCourse.CourseName
        });
        
        await _publishEndpoint.Publish<CourseUpdateEvent>(new
        {
            CourseId = updateCourse.CourseId,
            NewCoursePrice = updateCourse.CoursePrice,
            NewCourseImage = updateCourse.CourseImage,
            NewCourseName = updateCourse.CourseName
            
        });
        
        return ResponseDto<NoContent>.Success(204);
    }
    
    public async Task<ResponseDto<NoContent>> DeleteAsync(string id)
    {
        var result = await _courseCollection.DeleteOneAsync(course => course.CourseId == id);
        if (result.DeletedCount > 0)
        {
            return ResponseDto<NoContent>.Success(204);
        }
        else
        {
            return ResponseDto<NoContent>.Fail("Course not found", 404);
        }
    }
}