using Business.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OnlineStudyWeb.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class ChartController : Controller
{
   private readonly IOrderService _orderService;
   private readonly ICatalogService _catalogService;
   
    public ChartController(IOrderService orderService, ICatalogService catalogService)
    {
        _orderService = orderService;
        _catalogService = catalogService;
    }
    
    
    
    
    // GET
    public async Task<IActionResult> Index()
    {
        var orders = await _orderService.GetAllOrder();

        var orderCounts = orders
            .SelectMany(order => order.OrderItems) // Flatten the order items
            .GroupBy(item => item.ProductName) // Group by product name
            .Select(group => new
            {
                Name = group.Key, // Product name
                Count = group.Count() // Count of orders for this product
            })
            .ToList();

        return View(orderCounts);

    }

    public IActionResult Index2()
    {
        return View();
    }
    
    
    public async Task<IActionResult> OrderChart()
    {
        var courses = await _catalogService.GetCourseAsync();
        var jsonCourses = new List<object>();

        foreach (var course in courses)
        {
            var orders = await _orderService.GetAllOrder();// Sipariş verilerini asenkron olarak yükleyin

            var orderCount = orders
                .SelectMany(order => order.OrderItems) // Sipariş öğelerini düzleştirin
                .Count(item => item.ProductId == course.CourseId); // Bu kurs için sipariş sayısını sayın

            var jsonOrderCount = new
            {
                Name = course.CourseName, // Doğru kurs adı özelliğini kullanın
                Count = orderCount
            };

            jsonCourses.Add(jsonOrderCount);
        }

        return Json(new { jsonlist = jsonCourses });

    }

    public async Task<IActionResult> CategoryChart()
    {
        var categories = await _catalogService.GetCategoryAsync();
        var jsonCategories = new List<object>();
        
        foreach (var category in categories)
        {
            var courses = await _catalogService.GetCourseAsync();
            var courseCount = courses.Count(x => x.CategoryId == category.CategoryId);
            var jsonCategory = new
            {
                Name = category.CategoryName,
                Count = courseCount
            };
            jsonCategories.Add(jsonCategory);
        }
        
        return Json(new { jsonlist = jsonCategories });
        
    }
    

    
    public async Task<IActionResult> RevenueChart()
    {
        var orders = await _orderService.GetAllOrder();

        var revenueByMonth = orders
            .GroupBy(order => order.CreatedTime.Month) // Siparişleri aylara göre gruplayın
            .Select(group => new
            {
                Month = group.Key.ToString(),
                Revenue = group.Sum(order => order.TotalPrice)
            })
            .OrderBy(result => result.Month)
            .ToList();

        var months = revenueByMonth.Select(result => result.Month).ToArray();
        var revenues = revenueByMonth.Select(result => result.Revenue).ToArray();

        return Json(new { Months = months, Revenues = revenues });
    }




}