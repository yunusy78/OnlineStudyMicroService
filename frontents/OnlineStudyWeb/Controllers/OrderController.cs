using Business.Abstract;
using Business.Models.Order;
using Microsoft.AspNetCore.Mvc;
using OnlineStudyShared.Services;

namespace OnlineStudyWeb.Controllers;

public class OrderController : Controller
{
    private readonly ICartService _cartService;
    private readonly ISharedIdentity _sharedIdentity;
    private readonly IOrderService _orderService;
    
    public OrderController(ICartService cartService, ISharedIdentity sharedIdentity, IOrderService orderService)
    {
        _cartService = cartService;
        _sharedIdentity = sharedIdentity;
        _orderService = orderService;
    }
    
    
    // GET
    public IActionResult Checkout()
    {
        var userid = _sharedIdentity.GetUserId;
        var cart = _cartService.GetCart().Result;
        var email = _sharedIdentity.Email;
        var checkoutInfoInput = new OrderCheckOutInfoInput()
        {
            Cart = cart,
            UserId = userid,
            EmailAddress = email
        };


        return View(checkoutInfoInput);

    }
    
    [HttpPost]
    public async Task<IActionResult> Checkout(OrderCheckOutInfoInput orderCheckOutInfoInput)
    {
        // Senkronize olmayan microservislerde bu şekilde yapılabilir.
        //var orderStatus = await _orderService.CreateOrder(orderCheckOutInfoInput);
        var orderStatus = await _orderService.SuspectOrder(orderCheckOutInfoInput);
        if (!orderStatus.Success)
        {
            var cart = await _cartService.GetCart();
            ViewBag.Cart= cart;
            ViewBag.ErrorMessage = orderStatus.error;
            return View();
          
        }
        return RedirectToAction("Success", "Order" , new {orderid= new Random().Next(1,1000)});
    }
    
    public IActionResult Success(int orderid)
    {
        return View(orderid);
    }
}