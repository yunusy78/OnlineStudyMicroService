using AutoMapper;
using CatalogMicroServices.Business.Abstract;
using CatalogMicroServices.Dtos.Category;
using CatalogMicroServices.Model;
using CatalogMicroServices.Settings;
using MongoDB.Driver;
using OnlineStudyShared;

namespace CatalogMicroServices.Business.Concrete;

public class CategoryManager: ICategoryService
{
    private readonly IMongoCollection<Category> _categoryCollection;
    
    private readonly IMapper _mapper;
    
    public CategoryManager(IDatabaseSettings databaseSettings, IMapper mapper)
    {
        var client = new MongoClient(databaseSettings.ConnectionString);
        var database = client.GetDatabase(databaseSettings.DatabaseName);
        _categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);
        _mapper = mapper;
    }
    
    public async Task<ResponseDto<List<CategoryDto>>>GetAllAsync()
    {
        var categories = await _categoryCollection.Find(category => true).ToListAsync();
        return ResponseDto<List<CategoryDto>>.Success(_mapper.Map<List<CategoryDto>>(categories), 200);
    }

    
    public async Task<ResponseDto<CategoryDto>>CreateAsync(CreateCategoryDto t)
    {
       var newCategory= _mapper.Map<Category>(t);
       //newCategory.CategoryId = Guid.NewGuid().ToString();
        await _categoryCollection.InsertOneAsync(newCategory);
        return ResponseDto<CategoryDto>.Success(_mapper.Map<CategoryDto>(t), 200);
    }
    
    public async Task<ResponseDto<CategoryDto>>GetByIdAsync(string id)
    {
        var category = await _categoryCollection.Find<Category>(category => category.CategoryId == id).FirstOrDefaultAsync();
        if (category == null)
        {
            return ResponseDto<CategoryDto>.Fail("Category not found", 404);
        }
        return ResponseDto<CategoryDto>.Success(_mapper.Map<CategoryDto>(category), 200);
    }


}