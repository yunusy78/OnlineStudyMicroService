using Business.Models.Catalog;

namespace Business.Dtos.Catalog.Course;

public class UpdateCourseDto
{
    public string CourseId { get; set; }
    public string CourseName { get; set; }
    public string CourseDescription { get; set; }
    
    public string CourseImage { get; set; }
    
    public string UserId { get; set; }
    
    public decimal CoursePrice { get; set; }
    
    public DateTime CourseCreatedDate { get; set; }
    
    public string CategoryId { get; set; }
 
    
    public FeatureViewModel Feature { get; set; }
}