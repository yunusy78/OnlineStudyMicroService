using Business.Abstract;
using Business.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace OnlineStudyWeb.Controllers;

public class AuthController : Controller
{
    private readonly IIdentityService _identityService;
    
    public AuthController(IIdentityService identityService)
    {
        _identityService = identityService;
    }
    
    // GET
    public IActionResult SignIn()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> SignIn(SignInInput signInInput)
    {
        if(!ModelState.IsValid)
            return View();
        
        var response = await _identityService.SignIn(signInInput);
        if (response.IsSuccess)
        {
            return RedirectToAction("Index", "Home");
        }
        else
        {
            response.Errors.ForEach(x => ModelState.AddModelError(String.Empty, x));
            return View();
        }
        
    }
    
    public async Task<IActionResult> SignUp()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        await _identityService.RevokeRefreshToken();
        return RedirectToAction("Index", "Home");
    }
}