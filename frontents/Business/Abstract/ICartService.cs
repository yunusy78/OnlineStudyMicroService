using Business.Models.Cart;

namespace Business.Abstract;

public interface ICartService
{
    Task<bool> SaveOrUpdateCart(CartViewModel cartViewModel);
    Task<CartViewModel> GetCart();
    Task<bool> Delete();
    Task AddItem(CartItemViewModel cartItemViewModel);
    Task<bool> RemoveCartItem(string courseId);
    Task<bool> ApplyDiscount(string discountCode);
    Task<bool> CancelApplyDiscount();
}