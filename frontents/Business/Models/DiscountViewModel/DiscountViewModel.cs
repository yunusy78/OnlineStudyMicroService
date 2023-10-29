namespace Business.Models.DiscountViewModel;

public class DiscountViewModel
{
    
    public string UserId { get; set; }
    
    public string Code { get; set; }
    
    public int Rate { get; set; }
    
    public DateTime CreatedTime { get; set; }
    
    public bool Status { get; set; }
}