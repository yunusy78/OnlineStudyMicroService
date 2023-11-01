namespace StripePaymentMicroService.Dtos.OrderDto;

public class OrderPaymentDto
{
    public OrderPaymentDto()
    {
        OrderItems = new List<OrderItemDto>();
    }
    public string BuyerId { get; set; }
    public OrderAddressDto Address { get; set; }
    public List<OrderItemDto> OrderItems { get; set; }
    
    
}