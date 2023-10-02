using Entity.Concrete;
using OnlineStudyShared;

namespace Business.Abstract;

public interface IDiscountService
{
    Task<ResponseDto<List<Discount>>> GetAllAsync();
    Task<ResponseDto<Discount>> GetByIdAsync(int id);
    Task<ResponseDto<Discount>> AddAsync(Discount discount);
    Task<ResponseDto<Discount>> UpdateAsync(Discount discount);
    Task<ResponseDto<Discount>> DeleteAsync(int id);
    Task<ResponseDto<Discount>> GetByCodeAndUserAsync(string code, string userId);
    
}