using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Business.Abstract;
using Business.Models.Cart;
using OnlineStudyShared;
using OnlineStudyShared.Services;
namespace Business.Concrete;

public class CartManager : ICartService
{
    private readonly HttpClient _httpClient;
    private readonly ISharedIdentity _sharedIdentity;
    private readonly IDiscountService _discountService;
    
    public CartManager(HttpClient httpClient,ISharedIdentity sharedIdentity, IDiscountService discountService)
    {
        _httpClient = httpClient;
        _sharedIdentity = sharedIdentity;
        _discountService = discountService;
    } 
    
    
    public async Task<bool> SaveOrUpdateCart(CartViewModel cartViewModel)
    {
        
        cartViewModel.UserId = _sharedIdentity.GetUserId;
        var json = JsonSerializer.Serialize(cartViewModel);

        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("carts", content);

        if (response.IsSuccessStatusCode)
        {
            return response.IsSuccessStatusCode;
        }
        else
        {
            var error = await response.Content.ReadFromJsonAsync<ResponseDto<NoContent>>();
            throw new Exception();
        }
        
        
        return response.IsSuccessStatusCode;
       
    }

    public async Task<CartViewModel> GetCart()
    {
        var response = _httpClient.GetAsync("carts").Result;
        if (!response.IsSuccessStatusCode)
        {
            return null!;
        }
        
        var basketViewModel = await response.Content.ReadFromJsonAsync<ResponseDto<CartViewModel>>();

        return basketViewModel!.Data;
        
    }

    public async Task<bool> Delete()
    {
        var result = await _httpClient.DeleteAsync("carts");
        return result.IsSuccessStatusCode;
    }

    public async Task AddItem(CartItemViewModel cartItemViewModel)
    {
        var basket = await GetCart();

        if (basket != null)
        {
            if (!basket.CartItems.Any(x => x.CourseId == cartItemViewModel.CourseId))
            {
                basket.CartItems.Add(cartItemViewModel);
            }
        }
        else
        {
            basket = new CartViewModel();

            basket.CartItems.Add(cartItemViewModel);
        }

        await SaveOrUpdateCart(basket);
    }

    public async Task<bool> RemoveCartItem(string courseId)
    {
        var cart = await GetCart();
        if (cart == null)
        {
            return false;
        }
        var cartItem = cart.CartItems.FirstOrDefault(x => x.CourseId == courseId);
        if (cartItem == null)
        {
            return false;
        }
       var result= cart.CartItems.Remove(cartItem);
        
        if (!result)
        {
            return false;
        }
        
        if(!cart.CartItems.Any())
        {
            cart.DiscountCode = null;
        }
        
        return await SaveOrUpdateCart(cart);
    }

    public async Task<bool> ApplyDiscount(string discountCode)
    {
        await CancelApplyDiscount();
        var cart = await GetCart();
        if (cart == null )
        {
            return false;
        }
        var discount = await _discountService.GetDiscount(discountCode);
        if (discount == null)
        {
            return false;
        }
        cart.ApplyDiscount(discount.Code,discount.Rate);
         await SaveOrUpdateCart(cart);
         return true;
    }

    public async Task<bool> CancelApplyDiscount()
    {
        var cart = await GetCart();
        if (cart == null || cart.DiscountCode == null)
        {
            return false;
        }
        
        cart.CancelDiscount();
        await SaveOrUpdateCart(cart);
        return true;
        
    }
}