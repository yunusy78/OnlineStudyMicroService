using CartMicroServices.Dtos;
using CartMicroServices.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineStudyShared.Controller;
using OnlineStudyShared.Services;

namespace CartMicroServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : CustomBaseController
    {
        private readonly ICartService _cartService;
        private readonly ISharedIdentity _sharedIdentity;

        public CartsController(ICartService cartService, ISharedIdentity sharedIdentity)
        {
            _cartService = cartService;
            _sharedIdentity = sharedIdentity;
        }

        [HttpGet]
        public async Task<IActionResult> GetCart()
        {
            //var claims=User.Claims.ToList();
            var response = await _cartService.GetCart(_sharedIdentity.GetUserId);
            return CreateActionResultInstance(response);
        }

        [HttpPost]
        public async Task<IActionResult> SaveOrUpdateCart(CartDto cartDto)
        {
            cartDto.UserId = _sharedIdentity.GetUserId;
            var response = await _cartService.SaveOrUpdateCart(cartDto);
            return CreateActionResultInstance(response);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCart()
        {
            var response = await _cartService.DeleteCart(_sharedIdentity.GetUserId);
            return CreateActionResultInstance(response);
        }
        
    }
}
