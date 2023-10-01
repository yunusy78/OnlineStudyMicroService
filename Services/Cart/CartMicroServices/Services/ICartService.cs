using CartMicroServices.Dtos;
using OnlineStudyShared;

namespace CartMicroServices.Services;

public interface ICartService
{
    Task<ResponseDto<CartDto>> GetCart(string userId);
    Task<ResponseDto<bool>> SaveOrUpdateCart(CartDto cartDto);
    Task<ResponseDto<bool>> DeleteCart(string userId);
    
}