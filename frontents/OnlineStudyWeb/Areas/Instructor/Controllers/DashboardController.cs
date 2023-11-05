using Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace OnlineStudyWeb.Areas.Instructor.Controllers;
[Area("Instructor")]
public class DashboardController : Controller
{
    private readonly IOrderService _orderService;
    
    public DashboardController(IOrderService orderService)
    {
        _orderService = orderService;
    }
    
    // GET
    public async Task<IActionResult> Index()
    {
        var orders = await _orderService.GetOrders(); // 

        // Beregn ukentlig og månedlig salg basert på order-dataene
        var nå = DateTime.Now;
        var forrigeUke = nå.AddDays(-7);
        var forrigeMåned = nå.AddMonths(-1);

        var ukentligSalg = orders
            .Where(order => order.CreatedTime >= forrigeUke)
            .Sum(order => order.TotalPrice);

        var månedligSalg = orders
            .Where(order => order.CreatedTime >= forrigeMåned)
            .Sum(order => order.TotalPrice);

        ViewBag.UkentligSalg = ukentligSalg;
        ViewBag.MånedligSalg = månedligSalg;
        
        var forrigeUkeSalg = orders
            .Where(order => order.CreatedTime >= forrigeUke && order.CreatedTime < nå)
            .Sum(order => order.TotalPrice);

        var forrigeMånedSalg = orders
            .Where(order => order.CreatedTime >= forrigeMåned && order.CreatedTime < nå)
            .Sum(order => order.TotalPrice);

        var ukentligØkning = ukentligSalg - forrigeUkeSalg;
        var månedligØkning = månedligSalg - forrigeMånedSalg;
        
        ViewBag.UkentligØkning = ukentligØkning;
        ViewBag.MånedligØkning = månedligØkning;

        return View();
    }
}