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
        course.CourseImage = "default.jpg";
        course.CourseCreatedDate = DateTime.Now;
        course.UserId = _sharedIdentity.GetUserId;
        await _catalogService.CreateCourseAsync(course);
        return RedirectToAction("Index");
       
       
    }
}