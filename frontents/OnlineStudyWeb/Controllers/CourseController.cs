using Business.Abstract;
using Business.Dtos.Catalog.Course;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineStudyShared.Services;

namespace OnlineStudyWeb.Controllers;
[Authorize]
public class CourseController : Controller
{
    private readonly ICatalogService _catalogService;
    private readonly ISharedIdentity _sharedIdentity;
    
    public CourseController(ICatalogService catalogService, ISharedIdentity sharedIdentity)
    {
        _catalogService = catalogService;
        _sharedIdentity = sharedIdentity;
    }
    
    // GET
    public async Task<IActionResult> Index()
    {
        var catalog = await _catalogService.GetAllCourseByUserIdAsync(_sharedIdentity.GetUserId);
        return View(catalog);
    }
    
    public async Task<IActionResult> Create()
    {
        var categories = await _catalogService.GetCategoryAsync();
        ViewBag.Category = new SelectList(categories, "CategoryId", "CategoryName");
        return View();

    }
    
    [HttpPost]
    public async Task<IActionResult> Create(CreateCourseDto course)
    {
        
        course.CourseCreatedDate = DateTime.Now;
        course.UserId = _sharedIdentity.GetUserId;
        await _catalogService.CreateCourseAsync(course);
        return RedirectToAction("Index");
        
    }

    public async Task<IActionResult> Update(string id)
    {
        var course = await _catalogService.GetCourseByIdAsync(id);
        var categories = await _catalogService.GetCategoryAsync();
        ViewBag.Category = new SelectList(categories, "CategoryId", "CategoryName", course.CategoryId);
        if (course == null)
        {
            return NotFound();
            
        }
        
        UpdateCourseDto updateCourseDto = new UpdateCourseDto
        {
            CourseId = course.CourseId,
            CourseName = course.CourseName,
            CoursePrice = course.CoursePrice,
            CourseDescription = course.CourseDescription,
            Feature = course.Feature,
            CourseImage = course.CourseImage,
            CategoryId = course.CategoryId,
            UserId = course.UserId
        };
        return View(updateCourseDto);
        
    }
    
    [HttpPost]
    public async Task<IActionResult> Update(UpdateCourseDto updateCourseDto)
    {
        await _catalogService.UpdateCourseAsync(updateCourseDto);
        return RedirectToAction("Index");
    }
    
    public async Task<IActionResult> Delete(string id)
    {
        await _catalogService.DeleteCourseAsync(id);
        return RedirectToAction("Index");
    }
    
    
}