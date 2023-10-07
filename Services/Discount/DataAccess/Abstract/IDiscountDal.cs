using Entity.Concrete;
using OnlineStudyShared;

namespace DataAccess.Abstract;

public interface IDiscountDal
{
    Task<ResponseDto<List<Discount>>> GetAllAsync();
    Task<ResponseDto<Discount>> GetByIdAsync(int id);
    Task<ResponseDto<Discount>> AddAsync(Discount discount);
    Task<ResponseDto<Discount>> UpdateAsync(Discount discount);
    Task<ResponseDto<Discount>> DeleteAsync(int id);
    Task<ResponseDto<Discount>> GetByCodeAndUserAsync(string code, string userId);
    
}