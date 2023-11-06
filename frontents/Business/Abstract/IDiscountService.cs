using Business.Models.DiscountViewModel;

namespace Business.Abstract;

public interface IDiscountService
{
    Task<DiscountViewModel> GetDiscount(string code);
    Task<bool> CreateDiscount(DiscountViewModel discountViewModel);
    
    Task<List<DiscountUpdateViewModel>> GetAllDiscount();
    
    Task<bool> UpdateDiscount(DiscountUpdateViewModel discountViewModel);
    
    Task<bool> DeleteDiscount(int id);
    
    Task<DiscountUpdateViewModel> GetDiscountById(int id);
}