using OrderApplication.Dtos;

namespace Business.Models.Order;

public class OrderCreateInput
{
    public OrderCreateInput()
    {
        OrderItems = new List<OrderItemViewModel>();
    }

    public string BuyerId { get; set; }
    
    public AddressDto AddressDto { get; set; }
    
    public List<OrderItemViewModel> OrderItems { get; set; }
}