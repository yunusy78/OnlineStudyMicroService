using System.Linq.Expressions;
using System.Net.Http.Json;
using Business.Abstract;
using Business.Dtos.Contact;
using Business.Helpers;
using Business.Models;
using Microsoft.Extensions.Configuration;
using OnlineStudyShared;

namespace Business.Concrete;

public class InstructorManager : IInstructorService
{
    private readonly HttpClient _httpClient;
    private readonly IImageStockService _imageStockService;
    private readonly PhotoStockHelper _photoStockHelper;
    private readonly IConfiguration _configuration;
    
    public InstructorManager(HttpClient httpClient,
        IImageStockService imageStockService ,
        PhotoStockHelper photoStockHelper, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _imageStockService = imageStockService;
        _photoStockHelper = photoStockHelper;
        _configuration = configuration;
    }
    
    
    
    public async Task<bool> AddAsync(InstructorDto entity)
    {
        var imageResult = await _imageStockService.UploadImageCourseAsync(entity.ImageFormFile);
        if (imageResult != null)
        {
            entity.ImageUrl= imageResult.Url;
        }
        
        var response = await _httpClient.PostAsJsonAsync("instructors", entity);
        if (!response.IsSuccessStatusCode)
        {
            return false;
        }
        
        return true;
        
    }

    public async Task<bool> UpdateAsync(InstructorDto entity)
    {
        var imageResult = await _imageStockService.UploadImageCourseAsync(entity.ImageFormFile);
        if (imageResult != null)
        {
            entity.ImageUrl= imageResult.Url;
        }
        var response = await _httpClient.PutAsJsonAsync("instructors", entity);
        if (!response.IsSuccessStatusCode)
        {
            return false;
        }
        
        return true;
    }

    
    public async Task<bool> DeleteAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"instructors/{id}");
        if (!response.IsSuccessStatusCode)
        {
            return false;
        }
        
        return true;
    }

    public async Task<ResponseDto<List<InstructorDto>>> GetAllAsync()
    {
        var serviceApiSettings = _configuration.GetSection("ServiceApiSettings").Get<ServiceApiSettings>();
        var response = await _httpClient.GetAsync($"{serviceApiSettings!.ContactUri}instructors");
        if (!response.IsSuccessStatusCode)
        {
            return null!;
        }
        
        var instructors = await response.Content.ReadFromJsonAsync<ResponseDto<List<InstructorDto>>>();
        instructors!.Data.ForEach(x =>
        {
            x.ImageUrl = _photoStockHelper.GetPhotoStockUrl(x.ImageUrl);
        });
        return instructors!;
    }

    public async Task<ResponseDto<InstructorDto>> GetByIdAsync(int id)
    {
        var response = await _httpClient.GetAsync($"instructors/{id}");
        if (!response.IsSuccessStatusCode)
        {
            return null!;
        }
        
        var instructor = await response.Content.ReadFromJsonAsync<ResponseDto<InstructorDto>>();
        instructor!.Data.ImageUrl= _photoStockHelper.GetPhotoStockUrl(instructor.Data.ImageUrl);
        return instructor!;
    }

    public async Task<ResponseDto<List<InstructorDto>>> GetListByFilterAsync(Expression<Func<InstructorDto, bool>> filter)
    {
        var response = await _httpClient.GetAsync("instructors");
        if (!response.IsSuccessStatusCode)
        {
            return null!;
        }
        
        var instructors = await response.Content.ReadFromJsonAsync<ResponseDto<List<InstructorDto>>>();
        instructors!.Data.ForEach(x =>
        {
            x.ImageUrl = _photoStockHelper.GetPhotoStockUrl(x.ImageUrl);
        });
        return instructors!;
       
    }
}