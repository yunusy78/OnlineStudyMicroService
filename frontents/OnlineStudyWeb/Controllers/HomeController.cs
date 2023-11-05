using System.Diagnostics;
using Business.Abstract;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using OnlineStudyWeb.Models;

namespace OnlineStudyWeb.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ICatalogService _catalogService;
    private readonly ICartService _cartService;

    public HomeController(ILogger<HomeController> logger, ICatalogService catalogService, ICartService cartService)
    {
        _logger = logger;
        _catalogService = catalogService;
        _cartService = cartService;
    }
   

    public async Task<IActionResult> Index(string? search)
    {
        var courses = await _catalogService.GetCourseAsync();
        if (!string.IsNullOrEmpty(search))
        {
            courses = courses.Where(x => x.CourseName.ToLower().Contains(search.ToLower())).ToList();
        }
        return View(courses);
    }
    
    public async Task<IActionResult> CourseDetail(string id)
    {
        var result = await _catalogService.GetCourseByIdAsync(id);
        return View(result);
    }

    public IActionResult Privacy()
    {
        
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        var exceptionHandlerPathFeature =
            HttpContext.Features.Get<IExceptionHandlerPathFeature>();
        if (exceptionHandlerPathFeature != null && exceptionHandlerPathFeature.Error is UnauthorizedAccessException)
        {
            return RedirectToAction("SignUp", "Auth");
        }
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}