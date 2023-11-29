using System.Security.Claims;
using Business.Abstract;
using Business.Models.Cart;
using Business.Models.DiscountViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStudyShared.Services;
using Business.Models.DiscountViewModel;
using Business.Models.FakePayment;
using Business.Models.Order;
using Stripe.Checkout;

namespace OnlineStudyWeb.Controllers;

[Authorize]
public class CartController : Controller
{
    private readonly ICatalogService _catalogService;
    private readonly ICartService _cartService;
    private readonly ISharedIdentity _sharedIdentity;

    public CartController(ICatalogService catalogService, ICartService cartService, ISharedIdentity sharedIdentity)
    {
        _catalogService = catalogService;
        _cartService = cartService;
        _sharedIdentity = sharedIdentity;
    }

    // GET
    public async Task<IActionResult> Index()
    {
        var result = await _cartService.GetCart();

        if (result==null)
        {
            return RedirectToAction("Error2", "Home");
        }
        return View(result);
    }

    public async Task<IActionResult> AddToCart(string courseId)
    {

        var course = await _catalogService.GetCourseByIdAsync(courseId);
        if (course == null)
        {
            return RedirectToAction("Index", "Home");
        }

        var cartItemViewModel = new CartItemViewModel
        {
            CourseId = course.CourseId,
            CourseName = course.CourseName,
            Price = course.CoursePrice,
            PictureUrl = course.StockImageUrl,

            Quantity = 1
        };

        try
        {
            await _cartService.AddItem(cartItemViewModel);
            return RedirectToAction("Index");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return RedirectToAction("Error2", "Home");
        }
        
    }

    public async Task<IActionResult> RemoveCartItem(string courseId)
    {
        await _cartService.RemoveCartItem(courseId);
        return RedirectToAction("Index");
    }



    public async Task<IActionResult> ApplyDiscount(string discountCode)
    {
        var response = await _cartService.ApplyDiscount(discountCode);
        TempData["discountError"] = response;
        return RedirectToAction("Index");
    }



    public async Task<IActionResult> CancelApplyDiscount()
    {
        await _cartService.CancelApplyDiscount();
        return RedirectToAction("Index");
    }

   
    
    
    
}


