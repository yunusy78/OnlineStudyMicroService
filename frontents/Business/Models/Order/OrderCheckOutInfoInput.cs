using Business.Models.Cart;

namespace Business.Models.Order;

public class OrderCheckOutInfoInput
{
    public string Province { get; set; }
    
    public string District { get; set; }
    
    public string Street { get; set; }
    
    public string ZipCode { get; set; }
    
    public string Line { get; set; }
    
    public string CardNumber { get; set; }
    public string CardHolderName { get; set; }
    public string ExpirationMonth { get; set; }
    public string ExpirationYear { get; set; }
    public string Cvv { get; set; }
    
    public string EmailAddress { get; set; }
    
    public string UserId { get; set; }
    
    public CartViewModel Cart { get; set; }
    
}