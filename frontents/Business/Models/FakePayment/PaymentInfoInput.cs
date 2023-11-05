using Business.Models.Order;

namespace Business.Models.FakePayment;

public class PaymentInfoInput
{
    public string CardNumber { get; set; }
    public string CardHolderName { get; set; }
    public string ExpirationMonth { get; set; }
    public string ExpirationYear { get; set; }
    public string Cvv { get; set; }
    public decimal TotalPrice { get; set; }
    
    public OrderCreateInput Order { get; set; }
    
    
}