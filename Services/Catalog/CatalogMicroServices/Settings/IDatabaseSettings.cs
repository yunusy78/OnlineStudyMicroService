namespace CatalogMicroServices.Settings;

public interface IDatabaseSettings
{
    public string ConnectionString { get; set; }
    public string DatabaseName { get; set; }
    public string CategoryCollectionName { get; set; }
    public string CourseCollectionName { get; set; }
    
    
}