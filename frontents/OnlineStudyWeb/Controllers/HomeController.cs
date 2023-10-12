using System.Diagnostics;
using Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using OnlineStudyWeb.Models;

namespace OnlineStudyWeb.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ICatalogService _catalogService;

    public HomeController(ILogger<HomeController> logger, ICatalogService catalogService)
    {
        _logger = logger;
        _catalogService = catalogService;
    }
   

    public async Task<IActionResult> Index()
    {
        var result = await _catalogService.GetCourseAsync();
        return View(result);
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
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}