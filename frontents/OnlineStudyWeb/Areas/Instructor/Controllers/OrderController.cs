using Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace OnlineStudyWeb.Areas.Instructor.Controllers;
[Area("Instructor")]
public class OrderController : Controller
{
    private readonly IOrderService _orderService;
    
    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }
    
    // GET
    public async Task<IActionResult> Index()
    {
        var result = await _orderService.GetOrders();
        return View(result);
    }
}