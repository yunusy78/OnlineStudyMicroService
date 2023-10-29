namespace Business.Models.Order;

public class OrderViewModel
{
    public int Id { get; set; }
    
    public string BuyerId { get; set; }
    
    public List<OrderItemViewModel> OrderItems { get; set; }
    
    public decimal TotalPrice { get; set; }
}