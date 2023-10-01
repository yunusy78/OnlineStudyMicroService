using CatalogMicroServices.Dtos.Category;
using CatalogMicroServices.Dtos.Feature;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CatalogMicroServices.Dtos.Course;

public class CourseDto
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string CourseId { get; set; }
    public string CourseName { get; set; }
    public string CourseDescription { get; set; }
    public string CourseImage { get; set; }
    
    public string UserId { get; set; }
    
    [BsonRepresentation(BsonType.Decimal128)]
    public decimal CoursePrice { get; set; }
    
    [BsonRepresentation(BsonType.DateTime)]
    public DateTime CourseCreatedDate { get; set; }
    
    [BsonRepresentation(BsonType.ObjectId)]
    public string CategoryId { get; set; }
    [BsonIgnore]
    public CategoryDto Category { get; set; }
    
    public FeatureDto Feature { get; set; }
}