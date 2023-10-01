using System.Text.Json;
using CartMicroServices.Dtos;
using OnlineStudyShared;

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
        var existCart = await _redisService.GetDb().StringGetAsync(userId);
        if (string.IsNullOrEmpty(existCart))
        {
            return ResponseDto<CartDto>.Fail("Cart not found", 404);
        }
        return ResponseDto<CartDto>.Success(JsonSerializer.Deserialize<CartDto>(existCart), 200);
        
    }

    public async Task<ResponseDto<bool>> SaveOrUpdateCart(CartDto cartDto)
    {
        var status = await _redisService.GetDb().StringSetAsync(cartDto.UserId, JsonSerializer.Serialize(cartDto));
        if (!status)
        {
            return ResponseDto<bool>.Fail("An error occurred while saving the cart", 500);
        }
        return ResponseDto<bool>.Success(200);
    }

    public async Task<ResponseDto<bool>> DeleteCart(string userId)
    {
        var status = await _redisService.GetDb().KeyDeleteAsync(userId);
        if (!status)
        {
            return ResponseDto<bool>.Fail("Cart not found", 404);
        }
        return ResponseDto<bool>.Success(200);
        
    }
}