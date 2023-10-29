using Business.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStudyShared.Services;

namespace OnlineStudyWeb.ViewComponents;

public class ShoppingCartCount : Microsoft.AspNetCore.Mvc.ViewComponent
{
    private readonly ICartService _cartService;
    private readonly ISharedIdentity _sharedIdentity;
    
    public ShoppingCartCount(ICartService cartService, ISharedIdentity sharedIdentity)
    {
        _cartService = cartService;
        _sharedIdentity = sharedIdentity;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        return View(0);
        var cart = await _cartService.GetCart();
        int result = cart.CartItems.Count;
        return View(result);
    }
    
}