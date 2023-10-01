namespace CatalogMicroServices.Settings;

public class DatabaseSettings : IDatabaseSettings
{
    public string? ConnectionString { get; set; }="mongodb://localhost:27011";
    public string? DatabaseName { get; set; }="CourseDb";
    public string? CategoryCollectionName { get; set; }="Categories";
    public string? CourseCollectionName { get; set; } = "Course";
    
}

