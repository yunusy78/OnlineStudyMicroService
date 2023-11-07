using System.Linq.Expressions;
using System.Net.Http.Json;
using Business.Abstract;
using Business.Dtos.Contact;
using Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using OnlineStudyShared;

namespace Business.Concrete;

public class TestimonialManager : ITestimonialService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    
    public TestimonialManager(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }
    
    public async Task<bool> AddAsync(TestimonialDto entity)
    {
        
        var response = await _httpClient.PostAsJsonAsync("testimonials", entity);
        if (!response.IsSuccessStatusCode)
        {
            return false;
        }
        
        return true;
    }

    public async Task<bool> UpdateAsync(TestimonialDto entity)
    {
        var response = await _httpClient.PutAsJsonAsync("testimonials", entity);
        if (!response.IsSuccessStatusCode)
        {
            return false;
        }
        
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"testimonials/{id}");
        if (!response.IsSuccessStatusCode)
        {
            return false;
        }
        
        return true;
    }

    [AllowAnonymous]
    public async Task<ResponseDto<List<TestimonialDto>>> GetAllAsync()
    {
        var serviceApiSettings = _configuration.GetSection("ServiceApiSettings").Get<ServiceApiSettings>();
        var response =  await _httpClient.GetAsync($"{serviceApiSettings!.ContactUri}testimonials");
        if (!response.IsSuccessStatusCode)
        {
            return ResponseDto<List<TestimonialDto>>.Fail("Error", 500);
        }
        
        var responseSuccess = await response.Content.ReadFromJsonAsync<ResponseDto<List<TestimonialDto>>>();
        return ResponseDto<List<TestimonialDto>>.Success(responseSuccess.Data, 200);
        
    }

    public async Task<ResponseDto<TestimonialDto>> GetByIdAsync(int id)
    {
        var response = await _httpClient.GetFromJsonAsync<ResponseDto<TestimonialDto>>($"testimonials/{id}");
        if (response!.Data == null)
        {
            return ResponseDto<TestimonialDto>.Fail(response.Errors, 500);
        }
        
        var request =  ResponseDto<TestimonialDto>.Success(response.Data, 200);
        return request;
    }

    public async Task<ResponseDto<List<TestimonialDto>>> GetListByFilterAsync(Expression<Func<TestimonialDto, bool>> filter)
    {
        var response = await _httpClient.GetFromJsonAsync<ResponseDto<List<TestimonialDto>>>("testimonials");
        if (!response.IsSuccess)
        {
            return ResponseDto<List<TestimonialDto>>.Fail(response.Errors, 500);
        }
        
        var request =  ResponseDto<List<TestimonialDto>>.Success(response.Data, 200);
        
        return request;
        
    }
}