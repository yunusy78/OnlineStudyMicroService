using Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace OnlineStudyWeb.Areas.Instructor.Component;

public class UserComponent : ViewComponent
{
    private readonly IUserService _userService;
    
    public UserComponent(IUserService userService)
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