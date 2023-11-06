using System.Linq.Expressions;
using OnlineStudyShared;

namespace DataAccess.Abstract;

public interface IGenericDal<T> where T : class, new()
{
    Task<ResponseDto<T>> AddAsync(T entity);
    Task<ResponseDto<T>> UpdateAsync(T entity);
    Task<ResponseDto<T>> DeleteAsync(int id);
    Task<ResponseDto<List<T>>> GetAllAsync();
    Task<ResponseDto<T>> GetByIdAsync(int id);
    Task<ResponseDto<List<T>>> GetListByFilterAsync(Expression<Func<T, bool>> filter);
    
}