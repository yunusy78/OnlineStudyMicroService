using CatalogMicroServices.Dtos.Category;
using CatalogMicroServices.Dtos.Feature;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CatalogMicroServices.Dtos.Course;

public class CreateCourseDto
{
   
    public string CourseName { get; set; }
    public string CourseDescription { get; set; }
    public string CourseImage { get; set; }
    public string UserId { get; set; }
    
    public decimal CoursePrice { get; set; }
    public DateTime CourseCreatedDate { get; set; }
    public string CategoryId { get; set; }
    public FeatureDto Feature { get; set; }
}