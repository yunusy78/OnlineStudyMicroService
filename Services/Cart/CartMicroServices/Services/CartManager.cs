using System.Text.Json;
using CartMicroServices.Dtos;
using OnlineStudyShared;
using OnlineStudyShared.Message.Event;

namespace CartMicroServices.Services;

public class CartManager : ICartService
{
    private readonly RedisService _redisService;
    
    public CartManager(RedisService redisService)
    {
        _redisService = redisService;
    }
    
    public async Task<ResponseDto<CartDto>> GetCart(string userId)
    {
        var cartKey = "cart_" + userId;
        var existCart = await _redisService.GetDb().StringGetAsync(cartKey);
        if (string.IsNullOrEmpty(existCart))
        {
            return ResponseDto<CartDto>.Fail("Cart not found", 404);
        }
        return ResponseDto<CartDto>.Success(JsonSerializer.Deserialize<CartDto>(existCart), 200);
        
    }
    
    public async Task<ResponseDto<CartDto>> GetCart2(string cartKey)
    {
        var existCart = await _redisService.GetDb().StringGetAsync(cartKey);
        if (string.IsNullOrEmpty(existCart))
        {
            return ResponseDto<CartDto>.Fail("Cart not found", 404);
        }
        return ResponseDto<CartDto>.Success(JsonSerializer.Deserialize<CartDto>(existCart), 200);
        
    }
    
    
    public async Task<ResponseDto<bool>> SaveOrUpdateCart(CartDto cartDto)
    {
        var cartKey = "cart_" + cartDto.UserId;

        var carts = _redisService.GetDb().ListRange("carts");

        if (!carts.Any(cart => cart.ToString() == cartKey))
        {
            await _redisService.GetDb().ListRightPushAsync("carts", cartKey);
        }
        
        var status = await _redisService.GetDb().StringSetAsync(cartKey, JsonSerializer.Serialize(cartDto));
        if (!status)
        {
            return ResponseDto<bool>.Fail("An error occurred while saving the cart", 500);
        }
        return ResponseDto<bool>.Success(200);
    }

    public async Task<ResponseDto<bool>> DeleteCart(string userId)
    {
        var cartKey = "cart_" + userId;
        _redisService.GetDb().ListRemove("carts", cartKey);
        var status = await _redisService.GetDb().KeyDeleteAsync(cartKey);
        if (!status)
        {
            return ResponseDto<bool>.Fail("Cart not found", 404);
        }
        
        return ResponseDto<bool>.Success(200);
        
    }
    
    
}