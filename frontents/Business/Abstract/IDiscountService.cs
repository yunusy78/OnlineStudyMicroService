using Business.Models.DiscountViewModel;

namespace Business.Abstract;

public interface IDiscountService
{
    Task<DiscountViewModel> GetDiscount(string code);
}