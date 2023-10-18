using Business.Models.Cart;

namespace Business.Dtos.Cart;

public class CartDto
{
    public string UserId { get; set; }
    
    public string? DiscountCode { get; set; }
    
    public int? DiscountRate { get; set; }
    
    public List<CartItemViewModel> CartItems { get; set; }
}