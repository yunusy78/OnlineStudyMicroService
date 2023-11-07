using Business.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using OnlineStudyShared.Services;

namespace OnlineStudyWeb.ViewComponents;

public class ShoppingCartCount : Microsoft.AspNetCore.Mvc.ViewComponent
{
    private readonly ICartService _cartService;
    private readonly IUserService _userService;
    
    public ShoppingCartCount(ICartService cartService, IUserService userService)
    {
        _cartService = cartService;
        _userService = userService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        
        if (!User.Identity.IsAuthenticated)
        {
            return View(0);
        }

        var cart = await _cartService.GetCart();
        if (cart == null || cart.CartItems == null)
        {
            return View(0);
        }

        int count = cart.CartItems.Count;
        return View(count);

        
    }

    
}