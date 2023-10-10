using Business.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
}