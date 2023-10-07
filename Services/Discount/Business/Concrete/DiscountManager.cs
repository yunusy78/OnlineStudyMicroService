using Business.Abstract;
using DataAccess.Abstract;
using Entity.Concrete;
using OnlineStudyShared;

namespace Business.Concrete;

public class DiscountManager : IDiscountService
{
    private readonly IDiscountDal _discountDal;
    
    public DiscountManager(IDiscountDal discountDal)
    {
        _discountDal = discountDal;
    }
    
    public async Task<ResponseDto<List<Discount>>> GetAllAsync()
    {
        return await _discountDal.GetAllAsync();
        
    }

    public async Task<ResponseDto<Discount>> GetByIdAsync(int id)
    {
        return await _discountDal.GetByIdAsync(id);
    }

    public async Task<ResponseDto<Discount>> AddAsync(Discount discount)
    {
        return await _discountDal.AddAsync(discount);
    }

    public async Task<ResponseDto<Discount>> UpdateAsync(Discount discount)
    {
        return await _discountDal.UpdateAsync(discount);
    }

    public async Task<ResponseDto<Discount>> DeleteAsync(int id)
    {
        return await _discountDal.DeleteAsync(id);
    }

    public async Task<ResponseDto<Discount>> GetByCodeAndUserAsync(string code, string userId)
    {
        return await _discountDal.GetByCodeAndUserAsync(code, userId);
    }
}