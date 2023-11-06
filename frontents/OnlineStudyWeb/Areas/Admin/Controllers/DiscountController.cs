using Microsoft.AspNetCore.Mvc;

namespace OnlineStudyWeb.Areas.Admin.Controllers;

public class DiscountController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}