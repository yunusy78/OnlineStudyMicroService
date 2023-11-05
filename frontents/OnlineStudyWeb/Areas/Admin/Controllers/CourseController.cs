using Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace OnlineStudyWeb.Areas.Admin.Controllers;
[Area("Admin")]
public class CourseController : Controller
{
    private readonly ICatalogService _catalogService;
    
    public CourseController(ICatalogService catalogService)
    {
        _catalogService = catalogService;
    }
    
    // GET
    public async Task<IActionResult> Index()
    {
        var catalog = await _catalogService.GetCourseAsync();
        return View(catalog);
    }
}