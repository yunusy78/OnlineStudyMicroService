using Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace OnlineStudyWeb.Controllers;

public class CoursesController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ICatalogService _catalogService;
    private readonly ICartService _cartService;

    public CoursesController(ILogger<HomeController> logger, ICatalogService catalogService, ICartService cartService)
    {
        _logger = logger;
        _catalogService = catalogService;
        _cartService = cartService;
    }
    
    // GET
    public async Task<IActionResult> Index(int page=1)
    {
        var courses = await _catalogService.GetCourseAsync();
        return View(courses.ToPagedList(page, 6));
    }
}