namespace Entity.Concrete;

[Dapper.Contrib.Extensions.Table("discounts")]
public class Discount
{
    public int Id { get; set; }
    
    public string UserId { get; set; }
    
    public string Code { get; set; }
    
    public int Rate { get; set; }
    
    public DateTime CreatedTime { get; set; }
    
    public bool Status { get; set; }
}