using OnlineStudyShared;

namespace CatalogMicroServices.Business.Abstract;

public interface IGenericService<TDto,CreateTDto, T> where T : class
{
    Task<ResponseDto<List<TDto>>> GetAllAsync();
    Task<ResponseDto<TDto>> CreateAsync(CreateTDto t);
    Task<ResponseDto<TDto>> GetByIdAsync(string id);
}
