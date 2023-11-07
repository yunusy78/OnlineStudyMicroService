using Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace OnlineStudyWeb.Areas.Admin.Component;

public class AdminUserComponent : ViewComponent
{
    private readonly IUserService _userService;
    
    public AdminUserComponent(IUserService userService)
    {
        _userService = userService;
    }
    
    public async Task<IViewComponentResult> InvokeAsync()
    {
        
        var result = await _userService.GetUser();
        ViewBag.User = result.UserName;
        ViewBag.Email = result.Email;
        return View();
    }
    
}