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
    
    public async Task<bool> CreateDiscount(DiscountViewModel discountViewModel)
    {
        var response = await _httpClient.PostAsJsonAsync("discounts", discountViewModel);
        if (!response.IsSuccessStatusCode)
        {
            return false;
        }
        
        return true;
    }
    
    public async Task<List<DiscountUpdateViewModel>> GetAllDiscount()
    {
        var response = await _httpClient.GetAsync("discounts");
        if (!response.IsSuccessStatusCode)
        {
            return null;
        }
        
        var discount = await response.Content.ReadFromJsonAsync<ResponseDto<List<DiscountUpdateViewModel>>>();
        return discount!.Data;
    }
    
    public async Task<bool> UpdateDiscount(DiscountUpdateViewModel discountViewModel)
    {
        var response = await _httpClient.PutAsJsonAsync("discounts", discountViewModel);
        if (!response.IsSuccessStatusCode)
        {
            return false;
        }
        
        return true;
    }
    
    public async Task<bool> DeleteDiscount(int id)
    {
        var response = await _httpClient.DeleteAsync($"discounts/{id}");
        if (!response.IsSuccessStatusCode)
        {
            return false;
        }
        
        return true;
    }
    
    public async Task<DiscountUpdateViewModel> GetDiscountById(int id)
    {
        var response = await _httpClient.GetAsync($"discounts/{id}");
        if (!response.IsSuccessStatusCode)
        {
            return null;
        }
        
        var discount = await response.Content.ReadFromJsonAsync<ResponseDto<DiscountUpdateViewModel>>();
        return discount!.Data;
    }
    
    
}
