using Microsoft.AspNetCore.Mvc;

namespace OnlineStudyWeb.Areas.Admin.Controllers;

public class UsersController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}