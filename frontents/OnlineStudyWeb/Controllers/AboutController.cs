using Microsoft.AspNetCore.Mvc;

namespace OnlineStudyWeb.Controllers;

public class AboutController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}