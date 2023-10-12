using System.ComponentModel.DataAnnotations;
using Business.Models.Catalog;
using Microsoft.AspNetCore.Http;

namespace Business.Dtos.Catalog.Course;

public class UpdateCourseDto
{
    public string CourseId { get; set; }
    
    [Required (ErrorMessage = "Please enter a name")]
    public string CourseName { get; set; }
    
    [Required (ErrorMessage = "Please enter a description")]
    public string CourseDescription { get; set; }
    
    public string CourseImage { get; set; }
    
    public string UserId { get; set; }
    
    [Required (ErrorMessage = "Please select a price")]
    public decimal CoursePrice { get; set; }
    
    public DateTime CourseCreatedDate { get; set; }
    
    [Required (ErrorMessage = "Please select a category")]
    public string CategoryId { get; set; }
 
    
    public FeatureViewModel Feature { get; set; }
    
    public IFormFile ImageFormFile { get; set; }
}