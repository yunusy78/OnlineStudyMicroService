using Microsoft.AspNetCore.Mvc;

namespace OnlineStudyWeb.Controllers;

public class ContactController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}