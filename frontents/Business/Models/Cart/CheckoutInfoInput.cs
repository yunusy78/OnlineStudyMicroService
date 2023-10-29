namespace Business.Models.Cart;

public class CheckoutInfoInput
{
    public string EmailAddress { get; set; }
    public string UserId { get; set; }
    public CartViewModel Cart { get; set; }
    
}