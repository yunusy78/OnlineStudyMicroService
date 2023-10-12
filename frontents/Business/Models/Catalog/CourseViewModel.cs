namespace Business.Models.Catalog;

public class CourseViewModel
{
    
    public string CourseId { get; set; }
    public string CourseName { get; set; }
    public string CourseDescription { get; set; }
    
    public string CourseShortDescription { get=>CourseDescription.Length>30?CourseDescription.Substring(0,30)+"...":CourseDescription;}
    public string CourseImage { get; set; }
    
    public string UserId { get; set; }
    
    public string StockImageUrl { get; set; }
  
    public decimal CoursePrice { get; set; }
    
   
    public DateTime CourseCreatedDate { get; set; }
    
   
    public string CategoryId { get; set; }
 
    public CategoryViewModel Category { get; set; }
    
    public FeatureViewModel Feature { get; set; }
}