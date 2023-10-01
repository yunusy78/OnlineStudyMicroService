using CatalogMicroServices.Business.Abstract;
using CatalogMicroServices.Dtos.Course;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineStudyShared.Controller;

namespace CatalogMicroServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : CustomBaseController
    {
        private readonly ICourseService _courseService;
        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var courses = await _courseService.GetAllAsync();
            return CreateActionResultInstance(courses);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var course = await _courseService.GetByIdAsync(id);
            return CreateActionResultInstance(course);
        }
        
        [HttpGet]
        [Route("/api/[controller]/GetAllByUserId/{userId}")]
        public async Task<IActionResult> GetAllByUserId(string userId)
        {
            var courses = await _courseService.GetAllByUserAsync(userId);
            return CreateActionResultInstance(courses);
        }
        
        [HttpPost]
        public async Task<IActionResult> Create(CreateCourseDto course)
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
