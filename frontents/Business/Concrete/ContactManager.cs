using System.Linq.Expressions;
using System.Net.Http.Json;
using Business.Abstract;
using Business.Dtos.Contact;
using OnlineStudyShared;

namespace Business.Concrete;

public class ContactManager : IContactService
{
    private readonly HttpClient _httpClient;
    
    public ContactManager(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<ResponseDto<ContactDto>> AddAsync(ContactDto entity)
    {
        var response = await _httpClient.PostAsJsonAsync("contacts", entity);
        if (!response.IsSuccessStatusCode)
        {
            return null!;
        }
        
        var contact = await response.Content.ReadFromJsonAsync<ResponseDto<ContactDto>>();
        return contact!;
    }

    public async Task<ResponseDto<ContactDto>> UpdateAsync(ContactDto entity)
    {
        var response = await _httpClient.PutAsJsonAsync("contacts", entity);
        if (!response.IsSuccessStatusCode)
        {
            return null!;
        }
        
        var contact = await response.Content.ReadFromJsonAsync<ResponseDto<ContactDto>>();
        return contact!;
        
    }

    public async Task<ResponseDto<ContactDto>> DeleteAsync(int id)
    {
        
        var response = await _httpClient.DeleteAsync($"contacts/{id}");
        if (!response.IsSuccessStatusCode)
        {
            return null!;
        }
        
        var contact = await response.Content.ReadFromJsonAsync<ResponseDto<ContactDto>>();
        return contact!;
        
    }

    public async Task<ResponseDto<List<ContactDto>>> GetAllAsync()
    {
        var response = await _httpClient.GetAsync("contacts");
        if (!response.IsSuccessStatusCode)
        {
            return null!;
        }
        
        var contact = await response.Content.ReadFromJsonAsync<ResponseDto<List<ContactDto>>>();
        return contact!;
    }

    public async Task<ResponseDto<ContactDto>> GetByIdAsync(int id)
    {
        var response = await _httpClient.GetAsync($"contacts/{id}");
        if (!response.IsSuccessStatusCode)
        {
            return null!;
        }
        
        var contact = await response.Content.ReadFromJsonAsync<ResponseDto<ContactDto>>();
        return contact!;
    }

    public async Task<ResponseDto<List<ContactDto>>> GetListByFilterAsync(Expression<Func<ContactDto, bool>> filter)
    {
        var response = await _httpClient.GetAsync("contacts");
        if (!response.IsSuccessStatusCode)
        {
            return null!;
        }
        
        var contact = await response.Content.ReadFromJsonAsync<ResponseDto<List<ContactDto>>>();
        return contact!;
    }
}