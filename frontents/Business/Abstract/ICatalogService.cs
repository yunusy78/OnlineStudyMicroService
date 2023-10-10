using Business.Dtos.Catalog.Course;
using Business.Models.Catalog;

namespace Business.Abstract;

public interface ICatalogService
{
    Task<List<CourseViewModel>> GetCourseAsync();
    Task<List<CourseViewModel>> GetAllCourseByUserIdAsync(string userId);
    
    Task<List<CategoryViewModel>> GetCategoryAsync();
    Task<CourseViewModel> GetCourseByIdAsync(string courseId);
    Task<bool> CreateCourseAsync(CreateCourseDto createCourseDto);
    Task<bool> UpdateCourseAsync(UpdateCourseDto updateCourseDto);
    Task<bool> DeleteCourseAsync(string courseId);
}