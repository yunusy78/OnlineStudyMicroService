using Business.Abstract;
using Business.Models.DiscountViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace OnlineStudyWeb.Areas.Admin.Controllers;
[Area("Admin")]
[Authorize(Roles = "Admin")]
public class DiscountController : Controller
{
    private readonly IDiscountService _discountService;
    private readonly IUserService _identityService;
    
    public DiscountController(IDiscountService discountService, IUserService identityService)
    {
        _discountService = discountService;
        _identityService = identityService;
    }
    
    // GET
    public async Task<IActionResult> Create()
    {
        var users = await _identityService.GetAllUser();
        ViewBag.Users = new SelectList(users, "Id", "Email");
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(DiscountViewModel discountViewModel)
    {
        discountViewModel.CreatedTime = DateTime.UtcNow;
        discountViewModel.Status = true;
        var discount = await _discountService.CreateDiscount(discountViewModel);
        if (discount == null)
        {
            return NotFound();
        }
        return RedirectToAction("Index","Dashboard");
    }
    
    public async Task<IActionResult> Update(int id)
    {
        var users = await _identityService.GetAllUser();
        ViewBag.Users = new SelectList(users, "Id", "Email");
        var discount = await _discountService.GetDiscountById(id);
        if (discount == null)
        {
            return NotFound();
        }
        return View(discount);
    }
    
    [HttpPost]
    
    public async Task<IActionResult> Update(DiscountUpdateViewModel discountViewModel)
    {
        discountViewModel.CreatedTime = DateTime.UtcNow;
        var discount = await _discountService.UpdateDiscount(discountViewModel);
        if (discount == null)
        {
            return NotFound();
        }
        return RedirectToAction("Index","Dashboard");
    }
    
    public async Task<IActionResult> Delete(int id)
    {
         await _discountService.DeleteDiscount(id);
        
        return RedirectToAction("Index", "Dashboard");
    }
    
    
    public async Task<IActionResult> Index()
    {
        var discount = await _discountService.GetAllDiscount();
        if (discount == null)
        {
            return NotFound();
        }
        return View(discount);
    }
    
}