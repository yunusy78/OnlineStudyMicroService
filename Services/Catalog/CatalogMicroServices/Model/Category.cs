using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CatalogMicroServices.Model;

public class Category
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string CategoryId { get; set; }
    public string CategoryName { get; set; }
}