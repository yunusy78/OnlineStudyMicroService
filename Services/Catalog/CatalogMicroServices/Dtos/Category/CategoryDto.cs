using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CatalogMicroServices.Dtos.Category;

public class CategoryDto
{
   
    public string CategoryId { get; set; }
    public string CategoryName { get; set; }
}