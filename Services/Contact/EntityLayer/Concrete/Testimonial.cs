namespace EntityLayer.Concrete;

public class Testimonial
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string Comment { get; set; }
    public bool Status { get; set; }
    public DateTime CreatedDate { get; set; }
}