namespace CartMicroServices.Dtos;

public class CartItemDto
{
    public int CourseId { get; set; }
    public string CourseName { get; set; }
    public decimal Price { get; set; }
    public string PictureUrl { get; set; }
    public int Quantity { get; set; }
    
}