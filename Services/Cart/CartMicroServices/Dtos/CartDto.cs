namespace CartMicroServices.Dtos;

public class CartDto
{
    public string UserId { get; set; }
    public string? DiscountCode { get; set; }
    public int? DiscountRate { get; set; }
    public List<CartItemDto> cartItems { get; set; }
    public decimal TotalPrice { get; set; }
}