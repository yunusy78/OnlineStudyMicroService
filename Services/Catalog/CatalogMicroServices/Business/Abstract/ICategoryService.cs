using CatalogMicroServices.Dtos.Category;
using CatalogMicroServices.Model;
using OnlineStudyShared;

namespace CatalogMicroServices.Business.Abstract;

public interface ICategoryService : IGenericService<CategoryDto, CreateCategoryDto, Category>
{
  
}