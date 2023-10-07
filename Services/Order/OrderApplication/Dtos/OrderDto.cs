namespace OrderApplication.Dtos;

public class OrderDto
{
    public int Id { get; set; }
    public string BuyerId { get; set; }
    public AddressDto Address { get; set; }
    public List<OrderItemDto> OrderItems { get; set; }
    public decimal TotalPrice { get; set; }
    
}