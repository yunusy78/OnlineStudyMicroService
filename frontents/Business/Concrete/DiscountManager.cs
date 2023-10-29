using System.Net.Http.Json;
using Business.Abstract;
using Business.Models.DiscountViewModel;
using OnlineStudyShared;

namespace Business.Concrete;

public class DiscountManager : IDiscountService
{
    private readonly HttpClient _httpClient;
    
    public DiscountManager(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<DiscountViewModel> GetDiscount(string code)
    {
        var response = await _httpClient.GetAsync($"discounts/GetByUser/{code}");
        if (!response.IsSuccessStatusCode)
        {
            return null;
        }
        
        var discount = await response.Content.ReadFromJsonAsync<ResponseDto<DiscountViewModel>>();
        return discount!.Data;
    }
}
