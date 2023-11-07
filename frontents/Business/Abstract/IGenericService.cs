using System.Linq.Expressions;
using OnlineStudyShared;

namespace Business.Abstract;

public interface IGenericService <T> where T : class
{
    Task<bool> AddAsync(T entity);
    Task<bool> UpdateAsync(T entity);
    Task<bool> DeleteAsync(int id);
    Task<ResponseDto<List<T>>> GetAllAsync();
    Task<ResponseDto<T>> GetByIdAsync(int id);
    Task<ResponseDto<List<T>>> GetListByFilterAsync(Expression<Func<T, bool>> filter);
}
