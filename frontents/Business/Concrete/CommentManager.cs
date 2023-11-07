using System.Linq.Expressions;
using System.Net.Http.Json;
using Business.Abstract;
using Business.Dtos.Contact;
using OnlineStudyShared;

namespace Business.Concrete;

public class CommentManager : ICommentService
{
    private readonly HttpClient _httpClient;
    
    public CommentManager(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<bool> AddAsync(CommentDto entity)
    {
        var response = await _httpClient.PostAsJsonAsync("comments", entity);
        if (!response.IsSuccessStatusCode)
        {
            return false;
        }
        
        return true;
    }

    public async Task<bool> UpdateAsync(CommentDto entity)
    {
        var response = await _httpClient.PutAsJsonAsync("comments", entity);
        if (!response.IsSuccessStatusCode)
        {
            return false;
        }
        
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"comments/{id}");
        if (!response.IsSuccessStatusCode)
        {
            return false;
        }
        
        return true;
    }

    public async Task<ResponseDto<List<CommentDto>>> GetAllAsync()
    {
        var response = await _httpClient.GetAsync("comments");
        if (!response.IsSuccessStatusCode)
        {
            return null!;
            
        }
        
        var request =  response.Content.ReadFromJsonAsync<ResponseDto<List<CommentDto>>>();
        
        return request.Result;
        
    }

    public async Task<ResponseDto<CommentDto>> GetByIdAsync(int id)
    {
        var response = await _httpClient.GetAsync($"comments/{id}");
        if (!response.IsSuccessStatusCode)
        {
            return null!;
        }
        
        var request =  response.Content.ReadFromJsonAsync<ResponseDto<CommentDto>>();
        return request.Result;
    }

    public async Task<ResponseDto<List<CommentDto>>> GetListByFilterAsync(Expression<Func<CommentDto, bool>> filter)
    {
        var response = await _httpClient.GetAsync("comments");
        if (!response.IsSuccessStatusCode)
        {
            return null!;
        }
        
        var request =  response.Content.ReadFromJsonAsync<ResponseDto<List<CommentDto>>>();
        return request.Result;
    }
}