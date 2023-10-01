using CatalogMicroServices.Dtos.Category;
using CatalogMicroServices.Dtos.Course;
using CatalogMicroServices.Model;
using OnlineStudyShared;

namespace CatalogMicroServices.Business.Abstract;

public interface ICourseService : IGenericService<CourseDto, CreateCourseDto, Course>
{
    Task<ResponseDto<List<CourseDto>>> GetAllByUserAsync(string userId);
    Task<ResponseDto<NoContent>> UpdateAsync(CourseUpdateDto t);
    Task<ResponseDto<NoContent>> DeleteAsync(string id);

}