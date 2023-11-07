using Business.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OnlineStudyWeb.Areas.Admin.Controllers;
[Area("Admin")]
[Authorize(Roles = "Admin")]
public class AdminOrderController : Controller
{
    private readonly IOrderService _orderService;
    
    public AdminOrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }
    
    // GET
    public async Task<IActionResult> Index()
    {
        var orders = await _orderService.GetAllOrder();
        return View(orders);
    }
}