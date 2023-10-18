using Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace OnlineStudyWeb.Areas.Instructor.Controllers;
[Area("Instructor")]
public class ProfileController : Controller
{
    private readonly IUserService _userService;
    
    public ProfileController(IUserService userService)
    {
        _userService = userService;
    }
    
    // GET
    public async Task<IActionResult> Index()
    {
        var result = await _userService.GetUser();
        return View(result);
    }
    
}