using Business.Abstract;
using Frontents.Business.Dtos.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OnlineStudyWeb.Areas.Admin.Controllers;
[Area("Admin")]
[Authorize(Roles = "Admin")]
public class UsersController : Controller
{
    private readonly IUserService _identityService;
    
    public UsersController(IUserService identityService)
    {
        _identityService = identityService;
    }
    
    // GET
    public async Task<IActionResult> Index()
    {
        var users = await _identityService.GetAllUser();
        return View(users);
    }
}