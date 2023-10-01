using AutoMapper;
using CatalogMicroServices.Dtos.Category;
using CatalogMicroServices.Dtos.Course;
using CatalogMicroServices.Dtos.Feature;
using CatalogMicroServices.Model;

namespace CatalogMicroServices.Mapping;

public class GeneralMapping : Profile
{
    public GeneralMapping()
    {
        CreateMap<Category, CategoryDto>().ReverseMap();
        CreateMap<Feature, FeatureDto>().ReverseMap();  
        CreateMap<Course, CourseDto>().ReverseMap();
        CreateMap<Course, CreateCourseDto>().ReverseMap();
        CreateMap<Category, CreateCategoryDto>().ReverseMap();
        CreateMap<Course, CourseUpdateDto>().ReverseMap();
        CreateMap<CreateCategoryDto,Category>().ReverseMap();
        CreateMap<CreateCategoryDto,CategoryDto>().ReverseMap();
    }
    
    
}